namespace SInterpreter.Nodes
{
    public abstract class ExpressionNode<T>
    {
        public abstract T? Evaluate();
    }
}
