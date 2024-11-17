namespace SInterpreter.Nodes
{
    public class BooleanNode<T>(T value) : ExpressionNode<T>
    {
        public T Value { get; } = value;

        public override T? Evaluate()
        {
            return Value;
        }
    }
}
