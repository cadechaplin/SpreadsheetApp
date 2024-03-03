namespace SpreadsheetEngine;

/// <summary>
/// Node for multiplication.
/// </summary>
internal class MultiplicationNode : OperatorNode
{
    /// <inheritdoc/>
    public override double Evaluate()
    {
        var left = this.Left;
        var right = this.Right;
        if (left != null && right != null)
        {
            return left.Evaluate() * right.Evaluate();
        }
        else
        {
            throw new InvalidOperationException("Cannot evaluate subtraction: missing operands.");
        }
    }

}