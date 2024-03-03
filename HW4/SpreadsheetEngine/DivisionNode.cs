namespace SpreadsheetEngine;

internal class DivisionNode : OperatorNode
{
    public override double evaluate()
    {
        return Left.evaluate() / Right.evaluate();
    }

}