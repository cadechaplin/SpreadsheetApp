// <copyright file="VariableNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine;

/// <summary>
/// Tests for expression tree.
/// </summary>
internal class VariableNode : ExpressionNode
{
    internal Dictionary<string, double> RefrenceDictionary;

    /// <summary>
    /// Gets variable name.
    /// </summary>
    internal string? Name { get; init; }

    /// <inheritdoc/>
    public override double Evaluate()
    {
        var name = this.Name;
        if (name != null)
        {
            return this.RefrenceDictionary[name];
        }

        return 0;
    }
}