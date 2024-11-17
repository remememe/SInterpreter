namespace SInterpreter.Nodes
{
    public class ConstantNode<T>(T value) : ExpressionNode<T>
    {
        private readonly T value = value;

        public override T? Evaluate()
        {
            return value;
        }
    }
}
