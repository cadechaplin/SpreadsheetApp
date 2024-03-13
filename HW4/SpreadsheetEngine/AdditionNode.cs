namespace SpreadsheetEngine;
internal class AdditionNode : OperatorNode
{
    /// <summary>
    /// Operator character stored for reference.
    /// </summary>
    public static string Operator = "+";

    /// <summary>
    /// Precedence stored for reference.
    /// </summary>
    public static int Precedence = 0;

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
            return left.Evaluate() + right.Evaluate();
        }
        else
        {
            throw new InvalidOperationException("Cannot evaluate subtraction: missing operands.");
        }
    }

}