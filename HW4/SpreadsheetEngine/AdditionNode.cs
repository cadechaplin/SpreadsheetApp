// <copyright file="AdditionNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable CS0414 // Field is assigned but its value is never used
#pragma warning disable SA1401
namespace SpreadsheetEngine;

/// <summary>
/// Node for addition.
/// </summary>
internal class AdditionNode : OperatorNode
{
    /// <summary>
    /// Operator character stored for reference.
    /// </summary>
    public static char Operator = '+';

    /// <summary>
    /// Precedence stored for reference.
    /// </summary>
    public static int Precedence = 1;

    /// <summary>
    /// Associativity stored for reference.
    /// </summary>
    public static Associativity A = Associativity.Left;

    /// <inheritdoc/>
    public override double Evaluate()
    {
        var left = this.Left;
        var right = this.Right;
        if (left != null && right != null)
        {
            return left.Evaluate() + right.Evaluate();
        }
        else
        {
            throw new InvalidOperationException("Cannot evaluate addition: missing operands.");
        }
    }
}