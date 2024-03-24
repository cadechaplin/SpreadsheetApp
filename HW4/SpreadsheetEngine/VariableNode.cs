// <copyright file="VariableNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable CS0414 // Field is assigned but its value is never used
#pragma warning disable SA1401

namespace SpreadsheetEngine;

/// <summary>
/// Tests for expression tree.
/// </summary>
internal class VariableNode : ExpressionNode
{
    /// <summary>
    /// Dictionary for storing variable values.
    /// </summary>
    internal Dictionary<string, double>? ReferenceDictionary;

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
            double val = this.ReferenceDictionary[name];
            if (val == double.NaN)
            {
                throw new ArgumentException("Variable has no value (NaN).", nameof(val));
            }

            return this.ReferenceDictionary[name];
        }

        return double.NaN;
    }
}