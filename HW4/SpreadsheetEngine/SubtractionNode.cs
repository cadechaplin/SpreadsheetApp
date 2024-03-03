namespace SpreadsheetEngine;
internal class SubtractionNode : OperatorNode
{
    public override double evaluate()
    {
        return Left.evaluate() - Right.evaluate();
        
    }

}