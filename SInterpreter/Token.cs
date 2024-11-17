namespace SInterpreter
{
    public class Token
    {
        public Tokens Type { get; }
        public string Value { get; }
        public Token(Tokens Type, string Value)
        {
            this.Type = Type;
            this.Value = Value;
        }
        public Token(Tokens Type)
        {
            this.Type = Type;
            Value = this.Type.ToString();
        }
        public Token(string Value)
        {
            this.Value = Value;
            Type = Tokens.IDENTIFIER;
        }
        public Token()
        {
            Value = string.Empty;
            Type = Tokens.UNKNOWN;
        }
    }
}
