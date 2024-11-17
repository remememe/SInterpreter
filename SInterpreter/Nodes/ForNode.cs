using System;
using SInterpreter.ParsingNodes;

namespace SInterpreter.Nodes
{
    public class ForStatementNode<T>(ExpressionNode<double> initialization, ExpressionNode<double> condition, ExpressionNode<T>? update, int[] Body, Parser Pr) : ExpressionNode<T>
    {
        public ExpressionNode<double> Initialization { get; } = initialization;
        public ExpressionNode<double> Condition { get; } = condition;
        public ExpressionNode<T>? Update { get; } = update;
        public int[] BodyOfLoop = Body;
        public Parser Pr = Pr;
        public override T? Evaluate()
        {
            while (Convert.ToBoolean(Condition.Evaluate()))
            {
                Pr.Pos = BodyOfLoop[0];
                Pr.ParseCheck(BodyOfLoop);
                if (Update is BinaryOperatorNode<T> binaryNode)
                {
                    binaryNode.AssignToLeft(binaryNode.Evaluate());
                }
            }
            return default;

        }
    }
}
