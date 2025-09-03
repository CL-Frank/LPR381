using LPR381.Core;
using Microsoft.FSharp.Core;
using Microsoft.FSharp.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LPR381.UI.Core
{
    public static class FSharpInterop
    {
        public static bool TrySome<T>(FSharpOption<T> opt, out T value)
        {
            if (FSharpOption<T>.get_IsSome(opt)) 
            { 
                value = opt.Value; 
                return true; 
            }
            value = default!;
            return false;
        }

        public static (string CaseName, object[] Fields) ReadUnion(object du)
        {
            var uf = FSharpValue.GetUnionFields(du, du.GetType(), null);
            return (uf.Item1.Name, uf.Item2);
        }

        public static Dictionary<string, double> ToDict(object mapLike)
        {
            if (mapLike is IEnumerable<KeyValuePair<string, double>> kvs)
                return new Dictionary<string, double>(kvs);

            var dict = new Dictionary<string, double>();
            if (mapLike is System.Collections.IEnumerable en)
            {
                foreach (var item in en)
                {
                    if (item == null) continue;
                    var type = item.GetType();
                    var key = type.GetProperty("Key")?.GetValue(item)?.ToString();
                    var val = type.GetProperty("Value")?.GetValue(item);
                    if (!string.IsNullOrEmpty(key) && val != null)
                        dict[key] = Convert.ToDouble(val);
                }
            }
            return dict;
        }
        
        public static double[] MatrixVectorMultiply(double[,] matrix, double[] vector)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            if (cols != vector.Length)
                throw new ArgumentException("Matrix columns must match vector length");
                
            var result = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                result[i] = 0;
                for (int j = 0; j < cols; j++)
                {
                    result[i] += matrix[i, j] * vector[j];
                }
            }
            return result;
        }
    }
}