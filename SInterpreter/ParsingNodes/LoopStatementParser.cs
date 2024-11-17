using SInterpreter.Nodes;

namespace SInterpreter.ParsingNodes
{
    public class LoopStatementParser<T>
    {
        public static ForStatementNode<T>? ParseForLoop(Parser Pr)
        {
            if (CheckKeyWords.IsLoop(Pr.LT[Pr.Pos].Value))
            {
                Pr.Pos++;

                Pr.Check(Tokens.LPAREN);
                string variableName = VariableParser<object>.ParseAssignment(Pr);

                if (Pr.VM.TryGetValue(variableName, out object? variableValue) && variableValue is RefNode<double> refNode)
                {
                    ExpressionNode<double> init = new VariableNode<double>(Tokens.IDENTIFIER, variableName, refNode);

                    Pr.Check(Tokens.SEMICOLON);
                    ExpressionNode<double>? condition = VariableParser<double>.ParseExpression(Pr);

                    Pr.Check(Tokens.SEMICOLON);
                    ExpressionNode<T>? increment = VariableParser<T>.ParseIncrement(Pr);

                    Pr.Check(Tokens.RPAREN);
                    int[] body = FunctionsExtension.ParseBlockCode(Tokens.RBRACE, Pr);

                    Pr.Check(Tokens.RBRACE);
                    return new ForStatementNode<T>(init, condition!, increment, body, Pr);
                }
            }
            return null;
        }
    }
}
