using SInterpreter.Nodes;
using System;
using System.Collections.Generic;

namespace SInterpreter.ParsingNodes
{
    public static class PrimaryExpressionParser<T>
    {
        public static ExpressionNode<T>? ParseExpression(Parser parser, int precedence = 0)
        {
            ExpressionNode<T>? left = ParsePrimary(parser);

            while (parser.Pos < parser.LT.Length &&
                   CheckKeyWords.IsBinaryOperator(parser.LT[parser.Pos].Value) &&
                   ParserExtension.GetOperatorPrecedence(parser.LT[parser.Pos].Value) > precedence)
            {
                Token operatorToken = parser.LT[parser.Pos];
                parser.Pos++;
                ExpressionNode<T>? right = ParseExpression(parser, ParserExtension.GetOperatorPrecedence(operatorToken.Value));

                if (left != null && right != null)
                {
                    left = new BinaryOperatorNode<T>(operatorToken, left, right);
                }
            }

            return left;
        }

        public static ExpressionNode<T>? ParsePrimary(Parser parser)
        {
            Token currentToken = parser.LT[parser.Pos];
            bool isNegative = false;
            if (currentToken.Type == Tokens.MINUS)
            {
                isNegative = true;
                parser.Pos++;
                currentToken = parser.LT[parser.Pos]; 
            }
            switch (currentToken.Type)
            {
                case Tokens.LPAREN:
                    return ParseParenthesizedExpression(parser);
                case Tokens.STRING:
                    return parser.ParseStringNode<T>();
                case Tokens.IDENTIFIER:
                    ExpressionNode<T> identifierNode = ParseIdentifier(parser)!;

                    if (isNegative && identifierNode is VariableNode<T> numNode)
                    {
                        return new NumberNode<T>((T)(object)(-(double)(object)numNode.Evaluate()!));
                    }

                    return identifierNode;
                default:
                    if (CheckKeyWords.IsMethod(currentToken.Type.ToString()))
                    {
                        return ParseMethods(parser);
                    }
                    ExpressionNode<T>? numberNode = ParseNumberOrThrow(parser, currentToken);

                    if (isNegative && numberNode is NumberNode<T> num)
                    {
                        return new NumberNode<T>((T)(object)(-(double)(object)num.Evaluate()!));
                    }

                    return numberNode;
            }
        }

        private static ExpressionNode<T>? ParseParenthesizedExpression(Parser parser)
        {
            parser.Pos++;
            ExpressionNode<T>? expression = ParseExpression(parser, 0);

            if (parser.Pos >= parser.LT.Length || parser.LT[parser.Pos].Type != Tokens.RPAREN)
            {
                throw new InvalidOperationException("Mismatched parentheses.");
            }

            parser.Pos++;
            return expression;
        }

        private static ExpressionNode<T>? ParseNumberOrThrow(Parser parser, Token currentToken)
        {
            if (double.TryParse(currentToken.Value, out double numberValue))
            {
                parser.Pos++;
                return new NumberNode<T>((T)(object)numberValue);
            }

            throw new InvalidOperationException("Expected a primary expression.");
        }

        private static ExpressionNode<T>? ParseIdentifier(Parser parser)
        {
            string variableName = parser.LT[parser.Pos].Value;

            if (parser.VM.TryGetValue(variableName, out object? variableValue))
            {
                parser.Pos++;
                return CreateVariableNode(parser, variableName, variableValue);
            }

            throw new InvalidOperationException($"Variable '{variableName}' not found.");
        }

        private static ExpressionNode<T>? CreateVariableNode(Parser parser, string variableName, object? variableValue)
        {
            if (variableValue is RefNode<double> refNodeDouble)
            {
                RefNode<T> refNode = EnsureRefNodeExists(parser, variableName, refNodeDouble);
                return new VariableNode<T>(Tokens.IDENTIFIER, variableName, refNode);
            }
            else if (variableValue is RefNode<string> refNodeString)
            {
                RefNode<T> refNode = EnsureRefNodeExists(parser, variableName, refNodeString);
                return new VariableNode<T>(Tokens.IDENTIFIER, variableName, refNode);
            }
            else if (variableValue is RefNode<object> refNodeObject)
            {
                RefNode<T> refNode = EnsureRefNodeExists(parser, variableName, refNodeObject);
                return new VariableNode<T>(Tokens.IDENTIFIER, variableName, refNode);
            }
            throw new InvalidOperationException("Unsupported variable type.");
        }

        private static RefNode<T> EnsureRefNodeExists<U>(Parser parser, string variableName, RefNode<U> refNodeDouble)
        {
            if (parser.VM.TryGetValue(variableName, out object? existingRefValue))
            {
                if (typeof(T) == typeof(double) && existingRefValue is RefNode<double> existingDoubleNode)
                {
                    RefNode<T> newNode = new((T)(object)existingDoubleNode.Value);
                    parser.VM[variableName] = newNode;
                    return newNode;
                }
            }
            RefNode<T> refNode = new((T)(object)refNodeDouble.Value!);
            parser.VM[variableName] = refNode;
            return refNode;
        }
        private static ExpressionNode<T> ParseMethods(Parser parser)
        {
            string methodName = parser.LT[parser.Pos].Value;
            parser.Pos++;
            parser.Pos++;
            Queue<object> argumentsValues = new();
            while (parser.LT[parser.Pos].Type != Tokens.RPAREN)
            {
                var argumentNode = ParseExpression(parser)!.Evaluate() ?? throw new InvalidOperationException("Invalid argument in method call.");
                argumentsValues.Enqueue(argumentNode);

                if (parser.LT[parser.Pos].Type == Tokens.COMMA)
                    parser.Pos++;
            }

            parser.Pos++;

            object result = MethodInvoker.InvokeMethod(methodName, [.. argumentsValues]);

            return new NumberNode<T>((T)result);
        }
    }
}
