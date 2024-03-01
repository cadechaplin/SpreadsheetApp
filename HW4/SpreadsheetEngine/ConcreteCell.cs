// <copyright file="ConcreteCell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel;

namespace SpreadsheetEngine;

/// <summary>
/// Cell that can be used to create an instance of Cell.
/// </summary>
public class ConcreteCell : Cell
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConcreteCell"/> class.
    /// </summary>
    /// <param name="rowIndex">Integer that sets the row Index.</param>
    /// <param name="columnIndex">Integer that sets the column Index.</param>
    public ConcreteCell(int rowIndex, int columnIndex)
        : base(rowIndex, columnIndex)
    {
    }

    public event PropertyChangedEventHandler? ValuePropertyChanged = (sender, e) => { };

    private string _value;
    public override string Value
    {
        get => _value;
        protected internal set
        {
            _value = value;
            this.ValuePropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));

        }
    }
}