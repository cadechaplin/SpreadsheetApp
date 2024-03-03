namespace SpreadsheetEngine;
internal class AdditionNode : OperatorNode
{
    public override double evaluate()
    {
        return Left.evaluate() + Right.evaluate();
    }

}