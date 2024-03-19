namespace SpreadsheetEngine;
internal class PowerNode : OperatorNode
{
    /// <summary>
    /// Operator character stored for reference.
    /// </summary>
    public static char Operator = '^';

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
            return Math.Pow(left.Evaluate(), right.Evaluate());
        }
        else
        {
            throw new InvalidOperationException("Cannot evaluate power: missing operands.");
        }
    }

}