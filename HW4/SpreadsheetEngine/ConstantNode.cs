// <copyright file="ConstantNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine;
#pragma warning disable SA1401

/// <summary>
/// Node for Constants.
/// </summary>
internal class ConstantNode : ExpressionNode
{
    /// <summary>
    /// Value of constant stored in node.
    /// </summary>
    public double Value;

    /// <summary>
    /// Evaluates node.
    /// </summary>
    /// <returns>Value stored in node. </returns>
    public override double Evaluate()
    {
        return this.Value;
    }
}