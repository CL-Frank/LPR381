using LPR381.Core;
using LPR381.UI.Core;
using LPR381.UI.Models;
using System.Collections.Generic;

namespace LPR381.UI.Solvers
{
    public sealed class PrimalSimplexRunner : SolverRunner
    {
        public override string Key => "primal-simplex";
        public override string Display => "Primal Simplex";

        protected override SolveSummary Solve(LPFormulation model)
        {
            _iterations.Clear();
            AddCanonicalForm(model);
            
            // Validate if problem is suitable for Primal Simplex
            var validationResult = ValidateProblem(model);
            if (!validationResult.IsValid)
            {
                return new SolveSummary
                {
                    Message = validationResult.ErrorMessage,
                    IsOptimal = false
                };
            }
            
            var root = new PrimalSimplex(model);
            var summary = new SolveSummary();
            var stack = new Stack<(ITree<SimplexNode> node, int idx)>();
            stack.Push((root, 0));

            while (stack.Count > 0)
            {
                var (cur, idx) = stack.Pop();
                var item = cur.Item;

                var t = item.Tableau;
                var (stateCase, _) = FSharpInterop.ReadUnion(item.State);
                var title = stateCase == "Pivot" ? $"Tableau {idx} (Pivot)" : $"Tableau {idx} (Final)";
                
                // Fix row names: first row should be "z", rest should be "c1", "c2", etc.
                var fixedRowNames = new string[t.Values.GetLength(0)];
                fixedRowNames[0] = "z";
                for (int i = 1; i < fixedRowNames.Length; i++)
                    fixedRowNames[i] = $"c{i}";
                
                _iterations.Add(new IterationTableau
                {
                    Title = title,
                    Columns = t.ColumnNames,
                    Rows = fixedRowNames,
                    Values = t.Values
                });

                var provider = (ISimplexResultProvider)item;
                if (FSharpInterop.TrySome(provider.SimplexResult, out var res))
                {
                    var (caseName, fields) = FSharpInterop.ReadUnion(res);
                    switch (caseName)
                    {
                        case "Optimal":
                            summary.IsOptimal = true;
                            summary.Objective = (double)fields[2];
                            summary.VariableValues = FSharpInterop.ToDict(fields[1]);
                            var objTypeStr = model.ObjectiveType == ObjectiveType.Max ? "maximization" : "minimization";
                            summary.Message = $"Optimal solution found using Primal Simplex method for {objTypeStr} problem.";
                            break;
                        case "Unbounded":
                            summary.Message = $"Unbounded (variable {fields[0]}).";
                            break;
                        case "Infeasible":
                            summary.Message = $"Infeasible (constraint {fields[0]}).";
                            break;
                        default:
                            summary.Message = $"Result: {caseName}";
                            break;
                    }
                }

                var children = cur.Children;
                for (int i = children.Length - 1; i >= 0; --i)
                    stack.Push((children[i], idx + 1));
            }

            return summary;
        }
        
        private (bool IsValid, string ErrorMessage) ValidateProblem(LPFormulation model)
        {
            // Check for integer restrictions - Primal Simplex only handles continuous variables
            for (int i = 0; i < model.VarIntRestrictions.Length; i++)
            {
                if (model.VarIntRestrictions[i] != IntRestriction.Unrestricted)
                {
                    return (false, $"Primal Simplex cannot handle integer restrictions. Variable '{model.VarNames[i]}' has integer restriction. Use Branch & Bound or Cutting Plane instead.");
                }
            }
            
            // Check for infeasible starting point indicators
            var canonical = model.ToLPCanonical();
            
            // Check if RHS has negative values (indicates potential infeasible starting point)
            for (int i = 0; i < canonical.RHS.Count; i++)
            {
                if (canonical.RHS[i] < 0)
                {
                    return (false, "Primal Simplex requires a feasible starting point. Problem has negative RHS values after conversion to canonical form. Use Revised Dual Simplex instead.");
                }
            }
            
            // Check for unrestricted variables - basic Primal Simplex works better with positive variables
            bool hasUnrestrictedVars = false;
            for (int i = 0; i < model.VarSignRestrictions.Length; i++)
            {
                if (model.VarSignRestrictions[i] == SignRestriction.Unrestricted)
                {
                    hasUnrestrictedVars = true;
                    break;
                }
            }
            
            if (hasUnrestrictedVars)
            {
                return (false, "Primal Simplex works best with non-negative variables. Problem has unrestricted variables. Use Revised Simplex for better handling of unrestricted variables.");
            }
            
            return (true, string.Empty);
        }
    }
}
