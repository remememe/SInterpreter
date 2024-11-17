namespace SInterpreter.Nodes
{
    public class NumberNode<T>(T Value) : ExpressionNode<T>
    {
        private Node<T> Node = new Node<T>(Tokens.INTEGER, null, Value);
        public override T Evaluate() => Value;
    }
}
