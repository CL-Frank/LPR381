using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPR381.UI.Solvers
{
    public static class SolverRegistry
    {
        public sealed record Entry(string Key, string Display, Func<ISolverRunner> Factory);

        public static IReadOnlyList<Entry> Available { get; } = new List<Entry>
        {
            new("primal-simplex",  "Primal Simplex (Continuous LP, Feasible Start)",  () => new PrimalSimplexRunner()),
            new("revised-primal-simplex", "Revised Primal Simplex (Continuous LP)", () => new RevisedSimplexRunner(true)),
            new("revised-dual-simplex", "Revised Dual Simplex (Continuous LP, Auto Primal)", () => new RevisedSimplexRunner(false)),
            new("branch-and-bound", "Branch & Bound (Integer LP)",  () => new BranchAndBoundRunner()),
            new("cutting-plane",   "Cutting Plane (Integer LP)",   () => new CuttingPlaneRunner()),
            new("knapsack",        "Knapsack (Binary Variables)",        () => new KnapsackRunner())
        };
    }
}
