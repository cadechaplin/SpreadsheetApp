// <copyright file="Associativity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine;

/// <summary>
/// Operator character stored for reference.
/// </summary>
public enum Associativity
{
    /// <summary>
    /// Value for Left Associative.
    /// </summary>
    Left,

    /// <summary>
    /// Value for Right Associative.
    /// </summary>
    Right,

    /// <summary>
    /// Value for NonAssociative.
    /// </summary>
    NonAssociative,
}