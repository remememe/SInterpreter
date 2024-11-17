using System.Collections.Generic;

namespace SInterpreter
{
    public class CheckKeyWords
    {
        public static bool IsDigit(char ch) => char.IsDigit(ch);

        public static bool IsLetter(char ch) => char.IsLetter(ch);

        public static bool IsPunctuation(string ch) => punctuation.Contains(ch.ToString());

        public static bool IsKeyword(string word) => keywords.Contains(word.ToUpper());

        public static bool IsOperator(string ch) => binaryoperators.Contains(ch);

        public static bool IsLogicalKeyword(string ch) => logicalkeywords.Contains(ch);

        public static bool IsTwoCharOperator(string op) => twoCharOperators.Contains(op);

        public static bool IsBinaryOperator(string Bo) => binaryoperators.Contains(Bo);

        public static bool IsMethod(string M) => methods.Contains(M);

        public static bool IsBinaryOperatorMath(string Bom) => binaryoperatorsMath.Contains(Bom);

        public static bool IsType(string Tp) => types.Contains(Tp);

        public static bool IsLoop(string Lp) => loops.Contains(Lp);

        public static readonly HashSet<string> methods =
        [
            "PRINT","SQRT","POW","SIN","COS","RAN","LENGTH","LOG","ABS","MAX","MIN","MAX","TAN","ASIN","ACOS","ATAN"
        ];

        public static readonly HashSet<string> logicalkeywords =
        [
            "IF"
        ];

        public static readonly HashSet<string> keywords =
        [
            "IF", "ELSE", "FOR",
        ];

        public static readonly HashSet<string> binaryoperatorsMath =
        [
            "PLUS", "MINUS", "MULTIPLY", "DIVIDE"
        ];

        public static readonly HashSet<string> binaryoperators =
        [
            "PLUS", "MINUS", "MULTIPLY", "DIVIDE", "EQUAL", "NOT_EQUAL", "LESS_EQUAL", "GREATER_EQUAL", "LESS_THAN", "GREATER_THAN", "INCREMENT", "DECREMENT","PLUS_EQUALS","MINUS_EQUALS","ASSIGN"
        ];

        public static readonly HashSet<string> operators =
        [
            "PLUS", "MINUS", "MULTIPLY", "DIVIDE", "ASSIGN","LESS_THAN", "GREATER_THAN"
        ];

        public static readonly HashSet<string> twoCharOperators =
        [
            "EQUAL", "NOT_EQUAL", "LESS_EQUAL", "GREATER_EQUAL"
        ];

        public static readonly HashSet<string> punctuation =
        [
            "LPAREN", "RPAREN", "LBRACE", "RBRACE", "SEMICOLON", "COMMA", "QUOTATIONMARK"
        ];

        public static readonly HashSet<string> types =
        [
            "NUMBER", "IDENTIFIER", "STRING"
        ];

        public static readonly HashSet<string> loops =
        [
            "FOR"
        ];

        public static Tokens GetOperatorToken(string op)
        {
            return op switch
            {
                "PLUS" => Tokens.PLUS,
                "MINUS" => Tokens.MINUS,
                "MULTIPLY" => Tokens.MULTIPLY,
                "DIVIDE" => Tokens.DIVIDE,
                "ASSIGN" => Tokens.ASSIGN,
                "EQUAL" => Tokens.EQUAL,
                "NOT_EQUAL" => Tokens.NOT_EQUAL,
                "LESS_THAN" => Tokens.LESS_THAN,
                "GREATER_THAN" => Tokens.GREATER_THAN,
                "LESS_EQUAL" => Tokens.LESS_EQUAL,
                "GREATER_EQUAL" => Tokens.GREATER_EQUAL,
                "INCREMENT" => Tokens.INCREMENT,
                "DECREMENT" => Tokens.DECREMENT,
                "PLUS_EQUALS" => Tokens.PLUS_EQUALS,
                "MINUS_EQUALS" => Tokens.MINUS_EQUALS,
                _ => Tokens.UNKNOWN
            };
        }

        public static Tokens GetPunctuationToken(string punct)
        {
            return punct switch
            {
                "LPAREN" => Tokens.LPAREN,
                "RPAREN" => Tokens.RPAREN,
                "LBRACE" => Tokens.LBRACE,
                "RBRACE" => Tokens.RBRACE,
                "SEMICOLON" => Tokens.SEMICOLON,
                "COMMA" => Tokens.COMMA,
                "QUOTATIONMARK" => Tokens.QUOTATIONMARK,
                _ => Tokens.UNKNOWN
            };
        }

        public static Tokens GetKeywordToken(string keyword)
        {
            return keyword.ToUpper() switch
            {
                "IF" => Tokens.IF,
                "ELSE" => Tokens.ELSE,
                "FOR" => Tokens.FOR,
                _ => Tokens.UNKNOWN
            };
        }

        public static Tokens GetTypeToken(string type)
        {
            return type.ToUpper() switch
            {
                "NUMBER" => Tokens.NUMBER,
                "IDENTIFIER" => Tokens.IDENTIFIER,
                "STRING" => Tokens.STRING,
                _ => Tokens.UNKNOWN
            };
        }

        public static Tokens GetLoopToken(string loop)
        {
            return loop.ToUpper() switch
            {
                "FOR" => Tokens.FOR,
                _ => Tokens.UNKNOWN
            };
        }

        public static Tokens GetMethodToken(string method)
        {
            return method.ToUpper() switch
            {
                "PRINT" => Tokens.PRINT,
                "SQRT" => Tokens.SQRT,
                "POW" => Tokens.POW,
                "SIN" => Tokens.SIN,
                "COS" => Tokens.COS,
                "RAN" => Tokens.RAN,
                "LENGTH" => Tokens.LENGTH,
                "LOG" => Tokens.LOG,
                "ABS" => Tokens.ABS,
                "MAX" => Tokens.MAX,
                "MIN" => Tokens.MIN,
                "TAN" => Tokens.TAN,
                "ASIN" => Tokens.ASIN,
                "ACOS" => Tokens.ACOS,
                "ATAN" => Tokens.ATAN,
                _ => Tokens.UNKNOWN
            };
        }
    }
}