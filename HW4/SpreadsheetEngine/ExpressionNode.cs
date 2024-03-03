namespace SpreadsheetEngine;

/// <summary>
/// Parent node for all expressions.
/// </summary>
internal abstract class ExpressionNode
{
    /// <summary>
    /// Evaluates value of current node.
    /// </summary>
    /// <returns>Returns the value of this node and its children.</returns>
    public abstract double Evaluate();
}