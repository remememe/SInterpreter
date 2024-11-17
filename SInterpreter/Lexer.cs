using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SInterpreter
{
    public class Lexer
    {
        private readonly string[] _words;
        private int _pos = 0;

        public Lexer(string code)
        {
            _words = Regex.Split(code.Trim(), @"\s+");
        }

        public bool HasNext()
        {
            return _pos < _words.Length;
        }

        public void FindType(out Token tokenized)
        {
            string word = _words[_pos];
            _pos++;

            if (CheckKeyWords.IsKeyword(word))
            {
                tokenized = new Token(CheckKeyWords.GetKeywordToken(word));
            }
            else if (CheckKeyWords.IsPunctuation(word))
            {
                if (word == Tokens.QUOTATIONMARK.ToString())
                {
                    tokenized = new Token(Tokens.STRING, GetString());
                }
                else
                {
                    tokenized = new Token(CheckKeyWords.GetPunctuationToken(word));
                }
            }
            else if (CheckKeyWords.IsTwoCharOperator(word) || CheckKeyWords.IsOperator(word))
            {
                tokenized = new Token(CheckKeyWords.GetOperatorToken(word));
            }
            else if (CheckKeyWords.IsType(word))
            {
                tokenized = new Token(CheckKeyWords.GetTypeToken(word));
            }
            else if (int.TryParse(word, out int x))
            {
                tokenized = new Token(Tokens.INTEGER, word);
            }
            else if (CheckKeyWords.IsMethod(word))
            {
                tokenized = new Token(CheckKeyWords.GetMethodToken(word));
            }
            else
            {
                tokenized = new Token(Tokens.IDENTIFIER, word);
            }
        }

        private string GetString()
        {
            StringBuilder result = new StringBuilder();
            while (HasNext())
            {
                string word = _words[_pos++];
                if (word == Tokens.QUOTATIONMARK.ToString())
                {
                    break;
                }
                result.Append(word + " ");
            }

            return result.ToString().TrimEnd();
        }
    }
}
