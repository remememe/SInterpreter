using SInterpreter.Nodes;
using System;

namespace SInterpreter.ParsingNodes
{
    public static class ParserExtension
    {
        public static ExpressionNode<T> ParseNumberNode<T>(this Parser parser)
        {
            parser.Pos++;
            string value = parser.LT[parser.Pos - 1].Value;
            if (typeof(T) == typeof(double))
            {
                return new NumberNode<T>((T)(object)double.Parse(value));
            }

            throw new InvalidOperationException($"Failed to parse '{value}' to type {typeof(T)}.");
        }

        public static ExpressionNode<T> ParseStringNode<T>(this Parser parser)
        {
            parser.Pos++;
            string value = parser.LT[parser.Pos - 1].Value;
            try
            {
                return new StringNode<T>((T)(object)value);
            }
            catch
            {
                throw new InvalidOperationException($"Failed to parse '{value}' to type {typeof(T)}.");
            }
        }

        public static bool Check(this Parser parser, Tokens expectedToken)
        {
            if (parser.Curr.Type == expectedToken)
            {
                parser.Pos++;
                return true;
            }

            throw new Exception($"Invalid Token. Expected: {expectedToken}, but got: {parser.Curr.Type}");
        }

        public static int GetOperatorPrecedence(string operatorValue)
        {
            return operatorValue switch
            {
                "PLUS" => 1,
                "MINUS" => 1,
                "MULTIPLY" => 2,
                "DIVIDE" => 2,

                "LESS_THAN" => 1,
                "GREATER_THAN" => 1,
                "LESS_EQUAL" => 1,
                "GREATER_EQUAL" => 1,
                "EQUAL" => 1,
                "NOT_EQUAL" => 1,

                "ASSIGN" => 0,
                "PLUS_EQUALS" => 0,
                "MINUS_EQUALS" => 0,

                "INCREMENT" => 3,
                "DECREMENT" => 3,

                "AND" => -1,
                "OR" => -2,
                "NOT" => 3,

                _ => 0
            };
        }
    }
}
