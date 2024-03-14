namespace SpreadsheetEngine;

/// <summary>
/// Node for multiplication.
/// </summary>
internal class MultiplicationNode : OperatorNode
{
    /// <summary>
    /// Operator character stored for reference.
    /// </summary>
    public static string Operator = "*";

    /// <summary>
    /// Precedence stored for reference.
    /// </summary>
    public static int Precedence = 2;

    /// <summary>
    /// Associativity stored for reference.
    /// </summary>
    public static Associativity A = Associativity.Left;
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