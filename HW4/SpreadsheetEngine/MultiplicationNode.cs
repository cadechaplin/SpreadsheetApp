namespace SpreadsheetEngine;

internal class MultiplicationNode : OperatorNode
{
    public override double evaluate()
    {
        return Left.evaluate() * Right.evaluate();
    }

}