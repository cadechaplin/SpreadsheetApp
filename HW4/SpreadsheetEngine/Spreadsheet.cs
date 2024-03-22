// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine;

using System.ComponentModel;

// disabling underscore warnings
#pragma warning disable SA1309
/// <summary>
/// Class to contain cells and manipulate the values.
/// </summary>
public class Spreadsheet
{
    /// <summary>
    /// Cells contained by the spreadsheet.
    /// </summary>
#pragma warning disable SA1401
    // Want to be able to access this from outside the class.
    public readonly Cell[,] Cells;
#pragma warning restore SA1401
    private readonly int _columnCount;
    private readonly int _rowCount;

    /// <summary>
    /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
    /// </summary>
    /// <param name="row"> Amount of rows to initiate.</param>
    /// <param name="col"> Amount of columns to initiate.</param>
    public Spreadsheet(int row, int col)
    {
        if (row <= 0 || col <= 0)
        {
            throw new IndexOutOfRangeException("Row and column counts must be greater than zero.");
        }

        this.Cells = new Cell[row, col];
        this._columnCount = col;
        this._rowCount = row;

        // Initialize each inner array separately
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                // Pass row and column indices to the Cell constructor
                this.Cells[i, j] = new ConcreteCell(i, j);
                this.Cells[i, j].PropertyChanged += this.OnCellPropertyChanged;
            }
        }
    }

    

    /// <summary>
    /// Gets the column count.
    /// </summary>
    public int ColumnCount => this._columnCount;

    /// <summary>
    /// Gets the row count.
    /// </summary>
    public int RowCount => this._rowCount;

    /// <summary>
    /// Gets the column count.
    /// </summary>
    /// <param name="sender"> Integer to use for indexing the Cell row.</param>
    /// <param name="e"> Integer to use for indexing the Cell .</param>
    protected virtual void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        this.EvaluateCellValue((Cell)sender);
    }

    private void EvaluateCellValue(Cell changeCell)
    {
        if (changeCell.Text.Length == 0)
        {
            changeCell.Value = changeCell.Text;
            return;
        }

        if (changeCell.Text[0] == '=')
        {
            if (changeCell.Text.Length < 2)
            {
                return;
            }

            int rowFind;
            if (int.TryParse(changeCell.Text.Substring(2), out rowFind))
            {
                rowFind -= 1;
            }
            else
            {
                return;
            }

            int colFind = changeCell.Text[1] - 'A';
            if (colFind < 0 || colFind > this._columnCount || rowFind > this._rowCount)
            {
                changeCell.Value = "Cell referenced out of range.";

                // index out of range
                return;
            }

            Cell? found = this.GetCell(rowFind, colFind);
            if (found != null)
            {
                changeCell.Value = found.Value;
            }
        }
        else
        {
            changeCell.Value = changeCell.Text;
        }
    }

    /// <summary>
    /// Gets the column count.
    /// </summary>
    /// <param name="row"> Integer to use for indexing the Cell row.</param>
    /// <param name="col">Integer to use for indexing the Cell column.</param>
    /// <returns> Returns the cell corresponding to the index.</returns>
    private Cell? GetCell(int row, int col)
    {
        if (row > this._rowCount || col > this.ColumnCount)
        {
            return null;
        }

        return this.Cells[row, col];
    }


    /// <summary>
    /// Cell that can be used to create an instance of Cell.
    /// </summary>
    private class ConcreteCell : Cell
    {
        /// <summary>
        /// Event to fire when changing a property.
        /// </summary>
        public override string Value
        {
            get => this.StoredValue;

            set
            {
                if (this.StoredValue != value)
                {
                    this.StoredValue = value;
                    this.OnPropertyChanged(nameof(this.Value));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteCell"/> class.
        /// </summary>
        /// <param name="rowIndex">Integer that sets the row Index.</param>
        /// <param name="columnIndex">Integer that sets the column Index.</param>
        public ConcreteCell(int rowIndex, int columnIndex)
            : base(rowIndex, columnIndex)
        {
        }
    }
}