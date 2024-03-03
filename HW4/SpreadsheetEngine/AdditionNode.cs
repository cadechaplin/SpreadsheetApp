namespace SpreadsheetEngine;
internal class AdditionNode : OperatorNode
{
    public override double Evaluate()
    {
        return Left.Evaluate() + Right.Evaluate();
    }

}