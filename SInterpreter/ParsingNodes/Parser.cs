using System;
using SInterpreter.Nodes;
using SInterpreter.ParsingNodes;

namespace SInterpreter.ParsingNodes
{
    public class Parser(Token[] ListTokens, int Pos)
    {
        public VariablesManager VM = [];
        public readonly Token[] LT = ListTokens;
        public int Pos = Pos;
        public Token Curr => LT[Pos];
        public void ParseCheck()
        {
            while (Pos < LT.Length)
            {
                if (Tokens.IDENTIFIER == Curr.Type)
                {
                    VariableParser<object>.ParseAssignment(this);
                    continue;
                }
                else if (Tokens.FOR == Curr.Type)
                {
                    LoopStatementParser<double>.ParseForLoop(this)!.Evaluate();
                    if (Curr.Type == Tokens.RBRACE) Pos++;
                    continue;
                }
                else if (Tokens.IF == Curr.Type)
                {
                    IfStatementNode<double>.ParseStatment(this)!.Evaluate();
                    if (Pos != LT.Length && Curr.Type == Tokens.RBRACE) Pos++;
                    continue;
                }
                else if (CheckKeyWords.IsMethod(Curr.Value))
                {
                    string Name = Curr.Value;
                    Pos++;
                    ExpressionNode<object> parameter = PrimaryExpressionParser<object>.ParseExpression(this, 0)!;
                    MethodInvoker.InvokeMethod(Name, parameter.Evaluate());
                }
                else
                {
                    throw new InvalidOperationException($"Unexpected token '{Curr.Value}' of type '{Curr.Type}' at position {Pos}.");
                }
            }
        }
        public void ParseCheck(int[] range)
        {
            while (Pos < range[1])
            {
                if (Tokens.IDENTIFIER == Curr.Type)
                {
                    VariableParser<object>.ParseAssignment(this);
                    continue;
                }
                if (Tokens.FOR == Curr.Type)
                {
                    LoopStatementParser<double>.ParseForLoop(this)!.Evaluate();
                    continue;
                }
                else if (Tokens.IF == Curr.Type)
                {
                    IfStatementNode<double>.ParseStatment(this)!.Evaluate();
                    if (Pos != LT.Length && Curr.Type == Tokens.RBRACE) Pos++;
                    continue;
                }
                else if (CheckKeyWords.IsMethod(Curr.Value))
                {
                    string Name = Curr.Value;
                    Pos++;
                    ExpressionNode<object> parameter = PrimaryExpressionParser<object>.ParseExpression(this, 0)!;
                    MethodInvoker.InvokeMethod(Name, parameter.Evaluate());
                }
                else
                {
                    throw new InvalidOperationException($"Unexpected token '{Curr.Value}' of type '{Curr.Type}' at position {Pos}.");
                }
            }
        }
    }
}
