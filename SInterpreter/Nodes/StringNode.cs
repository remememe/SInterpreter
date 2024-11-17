namespace SInterpreter.Nodes
{
    public class StringNode<T>(T Value) : ExpressionNode<T>
    {
        private Node<T> Node = new(Tokens.STRING, null, Value);
        public override T Evaluate() => Value;
    }
}
