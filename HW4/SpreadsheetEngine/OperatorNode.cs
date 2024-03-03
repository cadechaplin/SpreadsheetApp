namespace SpreadsheetEngine;

internal abstract class OperatorNode : ExpressionNode
{
    public ExpressionNode Left { get; set; }
    public ExpressionNode Right { get; set; }
    
}