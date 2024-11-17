using SInterpreter.Nodes;
using System.Collections.Generic;

namespace SInterpreter.ParsingNodes
{
    public class IfStatementNode<T>
    {
        public static IfFunctionNode<T>? ParseStatment(Parser Pr)
        {
            Pr.Pos++;

            Pr.Check(Tokens.LPAREN);

            ExpressionNode<double>? condition = VariableParser<double>.ParseExpression(Pr);
            if (condition == null) return default;

            Pr.Check(Tokens.RPAREN);

            List<ExpressionNode<double>>? conditions = [condition];
            List<int[]> blockPoses = [ParseBlockCodePoses(Pr, Tokens.RBRACE)];

            if (Pr.Pos != Pr.LT.Length)
            {
                while (Pr.Curr.Type == Tokens.ELSEIF)
                {
                    Pr.Pos++;
                    Pr.Check(Tokens.LPAREN);
                    var elseifCondition = VariableParser<double>.ParseExpression(Pr);
                    if (elseifCondition != null)
                    {
                        conditions.Add(elseifCondition);
                        Pr.Check(Tokens.RPAREN);
                        blockPoses.Add(ParseBlockCodePoses(Pr, Tokens.RBRACE));
                    }
                }
                if (Pr.Curr.Type == Tokens.ELSE)
                {
                    Pr.Pos++;
                    conditions.Add(null!);
                    blockPoses.Add(ParseBlockCodePoses(Pr, Tokens.RBRACE));
                }
            }

            return new IfFunctionNode<T>(conditions, blockPoses, Pr);

        }
        private static int Statments(Parser Pr, int Stats = 0)
        {
            while (Pr.Curr.Type != Tokens.RBRACE)
            {
                Pr.Pos++;
            }
            if (Pr.Curr.Type == (Tokens.ELSEIF | Tokens.ELSE))
            {
                return Statments(Pr, Stats);
            }
            else
            {
                return Stats;
            }
        }
        private static int[] ParseBlockCodePoses(Parser Pr, Tokens EndToken)
        {
            int[] blockPositions = new int[2];

            int startPosition = Pr.Pos;

            while (Pr.Curr.Type != EndToken)
            {
                Pr.Pos++;
            }

            blockPositions[0] = startPosition + 1;
            blockPositions[1] = Pr.Pos - 1;

            Pr.Pos++;
            return blockPositions;
        }
    }
}
