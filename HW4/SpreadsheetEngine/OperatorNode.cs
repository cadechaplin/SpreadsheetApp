// <copyright file="OperatorNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine;

/// <summary>
/// Node for operators.
/// </summary>
internal abstract class OperatorNode : ExpressionNode
{
    /// <summary>
    /// Gets or sets left node.
    /// </summary>
    public ExpressionNode? Left { get; set; }

    /// <summary>
    /// Gets or sets right node.
    /// </summary>
    public ExpressionNode? Right { get; set; }
}