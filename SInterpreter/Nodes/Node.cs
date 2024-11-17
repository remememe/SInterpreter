using System;
namespace SInterpreter.Nodes
{
    public class Node<T>(Tokens Type, string? Name, T Value) : ExpressionNode<T>
    {
        public Tokens Type { get; } = Type;
        public T Value { get; } = Value;
        public string? Name { get; } = Name;
        public override T Evaluate()
        {
            if (Tokens.INTEGER == Type || Tokens.STRING == Type || Tokens.IDENTIFIER == Type)
            {
                return Value;
            }
            else
            {
                throw new Exception($"Nieobsługiwany typ tokena: {Type}");
            }
        }
    }
}
