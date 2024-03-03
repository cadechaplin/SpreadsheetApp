namespace SpreadsheetEngine;

internal class ConstantNode : ExpressionNode
{
    public double Value;
    public override double evaluate()
    {
        return Value;
    }
}