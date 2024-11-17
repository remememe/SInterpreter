namespace SInterpreter.Nodes
{
    public class RefNode<T>(T value)
    {
        public T Value { get; set; } = value;
    }
}
