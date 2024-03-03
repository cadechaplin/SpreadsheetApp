namespace SpreadsheetEngine;

internal class ConstantNode : ExpressionNode
{
    public double Value;
    public override double Evaluate()
    {
        return Value;
    }
}