using SInterpreter.Nodes;
using System;

namespace SInterpreter.ParsingNodes
{
    public class VariableParser<T>
    {
        public static string ParseAssignment(Parser Pr)
        {
            string variableName = Pr.LT[Pr.Pos].Value;
            Pr.Pos++;

            if (Pr.LT[Pr.Pos].Type == Tokens.ASSIGN)
            {
                Pr.Pos++;
            }
            else
            {
                throw new InvalidOperationException("Expected assignment token.");
            }

            ExpressionNode<object>? expression = PrimaryExpressionParser<object>.ParseExpression(Pr, 0);
            if (expression != null)
            {
                object value = expression.Evaluate()!;

                if (value is double doubleValue)
                {
                    AssignValue(Pr, doubleValue, variableName);
                    return variableName;
                }
                else if (value is string stringValue)
                {
                    AssignValue(Pr, stringValue, variableName);
                    return variableName;
                }
                else
                {
                    throw new InvalidOperationException("Unsupported variable type.");
                }
            }

            throw new InvalidOperationException("Failed to parse assignment or expression.");
        }
        private static ExpressionNode<U> AssignValue<U>(Parser Pr, U value, string variableName)
        {
            RefNode<U> refValue = new(value);
            Pr.VM[variableName] = refValue;

            return new VariableNode<U>(Tokens.IDENTIFIER, variableName, refValue);
        }
        public static ExpressionNode<object>? ParseVariable(Parser Pr)
        {
            string variableName = Pr.LT[Pr.Pos].Value;
            if (Pr.VM.TryGetValue(variableName, out object value))
            {
                Pr.Pos++;
                return value switch
                {
                    double doubleValue => new NumberNode<object>(doubleValue),
                    string stringValue => new NumberNode<object>(stringValue),
                    _ => throw new InvalidOperationException("Unsupported variable type.")
                };
            }
            throw new Exception("Variable not found.");
        }

        public static ExpressionNode<T>? ParseExpression(Parser Pr, int precedence = 0)
        {
            ExpressionNode<T>? left = PrimaryExpressionParser<T>.ParsePrimary(Pr);
            while (Pr.Pos < Pr.LT.Length && CheckKeyWords.IsBinaryOperator(Pr.LT[Pr.Pos].Value) && ParserExtension.GetOperatorPrecedence(Pr.LT[Pr.Pos].Value) > precedence)
            {
                Token operatorToken = Pr.Curr;
                Pr.Pos++;
                ExpressionNode<T>? right = ParseExpression(Pr, ParserExtension.GetOperatorPrecedence(operatorToken.Value));
                if (left != null && right != null)
                    left = new BinaryOperatorNode<T>(operatorToken, left, right);
            }
            return left;
        }
        public static ExpressionNode<T>? ParseIncrement(Parser Pr)
        {
            string variableName = Pr.LT[Pr.Pos].Value;
            Pr.Pos++;

            if (Pr.LT[Pr.Pos].Type == Tokens.INCREMENT)
            {
                Pr.VM.TryGetValue(variableName, out object val);
                Pr.Pos++;
                RefNode<T> refNode;
                if (val is RefNode<T> existingRefNode)
                {
                    refNode = existingRefNode;
                }
                else
                {
                    refNode = new RefNode<T>((T)val);
                    Pr.VM[variableName] = refNode;
                }
                return new BinaryOperatorNode<T>(
                    new Token(Tokens.PLUS),
                    new VariableNode<T>(Tokens.IDENTIFIER, variableName, refNode),
                    new NumberNode<T>((T)Convert.ChangeType(1, typeof(T)))
                );
            }
            else if (Pr.LT[Pr.Pos].Type == Tokens.PLUS_EQUALS)
            {
                Pr.Pos++;
                ExpressionNode<T> incrementValue = PrimaryExpressionParser<T>.ParseExpression(Pr)!;
                Pr.VM.TryGetValue(variableName, out object val);
                RefNode<T> refNode;
                if (val is RefNode<T> existingRefNode)
                {
                    refNode = existingRefNode;
                }
                else
                {
                    refNode = new RefNode<T>((T)val);
                    Pr.VM[variableName] = refNode;
                }
                return new BinaryOperatorNode<T>(
                    new Token(Tokens.PLUS),
                    new VariableNode<T>(Tokens.IDENTIFIER, variableName, refNode),
                    incrementValue
                );
            }
            else if (Pr.LT[Pr.Pos].Type == Tokens.DECREMENT)
            {
                Pr.VM.TryGetValue(variableName, out object val);
                Pr.Pos++;
                RefNode<T> refNode;
                if (val is RefNode<T> existingRefNode)
                {
                    refNode = existingRefNode;
                }
                else
                {
                    refNode = new RefNode<T>((T)val);
                    Pr.VM[variableName] = refNode;
                }
                return new BinaryOperatorNode<T>(
                    new Token(Tokens.MINUS),
                    new VariableNode<T>(Tokens.IDENTIFIER, variableName, refNode),
                    new NumberNode<T>((T)Convert.ChangeType(1, typeof(T)))
                );
            }
            else if (Pr.LT[Pr.Pos].Type == Tokens.MINUS_EQUALS)
            {
                Pr.Pos++;
                ExpressionNode<T> decrementValue = PrimaryExpressionParser<T>.ParseExpression(Pr)!;
                Pr.VM.TryGetValue(variableName, out object val);
                RefNode<T> refNode;
                if (val is RefNode<T> existingRefNode)
                {
                    refNode = existingRefNode;
                }
                else
                {
                    refNode = new RefNode<T>((T)val);
                    Pr.VM[variableName] = refNode;
                }
                return new BinaryOperatorNode<T>(
                    new Token(Tokens.MINUS),
                    new VariableNode<T>(Tokens.IDENTIFIER, variableName, refNode),
                    decrementValue
                );
            }
            else
            {
                throw new InvalidOperationException("Unknown increment operator.");
            }
        }
        public static ExpressionNode<T> ParseNumberNode(Parser Pr)
        {
            Pr.Pos++;
            T val = (T)(object)Pr.LT[Pr.Pos - 1].Value;
            return new NumberNode<T>(val);
        }
    }
}
