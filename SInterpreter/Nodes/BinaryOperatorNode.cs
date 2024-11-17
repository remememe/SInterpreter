using System;
namespace SInterpreter.Nodes
{
    public class BinaryOperatorNode<T>(Token binaryToken, ExpressionNode<T> leftOperand, ExpressionNode<T> rightOperand) : ExpressionNode<T>
    {
        public Token BinaryToken { get; } = binaryToken;
        public ExpressionNode<T> LeftOperand { get; } = leftOperand;
        public ExpressionNode<T> RightOperand { get; } = rightOperand;

        public override T Evaluate()
        {
            dynamic leftValue = LeftOperand.Evaluate()!;
            dynamic rightValue = RightOperand.Evaluate()!;
            if (CheckKeyWords.IsBinaryOperatorMath(BinaryToken.Value))
            {
                return BinaryToken.Type switch
                {
                    Tokens.PLUS => leftValue + rightValue,
                    Tokens.MINUS => leftValue - rightValue,
                    Tokens.MULTIPLY => leftValue * rightValue,
                    Tokens.DIVIDE => rightValue == 0 ? throw new DivideByZeroException("Cannot divide by zero") : leftValue / rightValue,
                    _ => throw new Exception($"Unknown binary operator: {BinaryToken.Type}")
                };
            }
            else
            {
                return BinaryToken.Type switch
                {
                    Tokens.EQUAL => (T)(object)(leftValue == rightValue ? 1.0 : 0.0),
                    Tokens.NOT_EQUAL => (T)(object)(leftValue != rightValue ? 1.0 : 0.0),
                    Tokens.LESS_THAN => (T)(object)(leftValue < rightValue ? 1.0 : 0.0),
                    Tokens.LESS_EQUAL => (T)(object)(leftValue <= rightValue ? 1.0 : 0.0),
                    Tokens.GREATER_THAN => (T)(object)(leftValue > rightValue ? 1.0 : 0.0),
                    Tokens.GREATER_EQUAL => (T)(object)(leftValue >= rightValue ? 1.0 : 0.0),
                    _ => throw new Exception($"Unknown binary operator: {BinaryToken.Type}")
                };
            }
        }

        public void AssignToLeft(T value)
        {
            if (LeftOperand is IVariable<T> variableNode)
            {
                variableNode.AssignValue(value);
            }
            else
            {
                throw new Exception("Left operand is not a variable");
            }
        }
    }
}
