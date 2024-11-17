namespace SInterpreter.Nodes
{
    public class VariableNode<T>(Tokens Type, string Name, RefNode<T> Value) : ExpressionNode<T>,IVariable<T>
    {
        public Tokens Type { get; } = Type;
        public string? Name { get; } = Name;

        public RefNode<T> Val { get; } = Value;
        public override T? Evaluate()
        {
            return Val.Value;
        }
        public void AssignValue(T value)
        {
            Val.Value = value;
        }

        public string GetName()
        {
            return Name!;
        }
    }
}
