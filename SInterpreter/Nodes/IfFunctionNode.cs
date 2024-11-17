using System;
using System.Collections.Generic;
using SInterpreter.ParsingNodes;

namespace SInterpreter.Nodes
{
    public class IfFunctionNode<T>(List<ExpressionNode<double>>? conditions, List<int[]> poses, Parser pr) : ExpressionNode<T>
    {
        public List<ExpressionNode<double>>? Conditions { get; } = conditions;
        public List<int[]> Poses { get; } = poses;
        public Parser Pr { get; } = pr;

        public override T? Evaluate()
        {
            if (Conditions == null) return default;
            for (int i = 0; i < Conditions.Count; i++)
            {
                if (Conditions[i] == null || Convert.ToBoolean(Conditions[i].Evaluate()))
                {
                    int[] row = GetRow(Poses, i);
                    Pr.Pos = row[0];
                    Pr.ParseCheck(row);
                    Pr.Pos = Poses[^1][1] + 1;
                    return default;
                }
            }
            return default;
        }

        private static int[] GetRow(List<int[]> matrix, int rowIndex)
        {
            return matrix[rowIndex];
        }
    }
}
