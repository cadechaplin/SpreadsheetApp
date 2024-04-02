// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace SpreadsheetEngine;

using System.ComponentModel;
using System.Globalization;

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
    private Stack<Command> redo;
    private Stack<Command> undo;
#pragma warning restore SA1401

    /// <summary>
    /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
    /// </summary>
    /// <param name="row"> Amount of rows to initiate.</param>
    /// <param name="col"> Amount of columns to initiate.</param>
    public Spreadsheet(int row, int col)
    {
        redo = new Stack<Command>();
        undo = new Stack<Command>();
        if (row <= 0 || col <= 0)
        {
            throw new IndexOutOfRangeException("Row and column counts must be greater than zero.");
        }

        this.Cells = new Cell[row, col];
        this.ColumnCount = col;
        this.RowCount = row;

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
    public int ColumnCount { get; }

    /// <summary>
    /// Gets the row count.
    /// </summary>
    public int RowCount { get; }

    /// <summary>
    /// Gets the column count.
    /// </summary>
    /// <param name="sender"> Integer to use for indexing the Cell row.</param>
    /// <param name="e"> Integer to use for indexing the Cell .</param>
    protected virtual void OnCellPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        this.EvaluateCellValue((Cell)sender);
        if (sender is ConcreteCell cell)
        {
            foreach (var item in cell.ReferencedBy)
            {
                    this.EvaluateCellValue(item);
            }
        }
    }

    public void Undo()
    {
        if (undo.Count == 0)
        {
            return;
        }

        var temp = undo.Pop();
        temp.unexecute();
        redo.Push(temp);
    }
    public void Redo()
    {
        if (redo.Count == 0)
        {
            return;
        }

        var temp = redo.Pop();
        temp.execute();
        undo.Push(temp);
    }

    public void RequestColorChange(List<Cell> cellsChanged, uint next)
    {
        List<uint> prev = new List<uint>();
        foreach (Cell cell in cellsChanged)
        {
            prev.Add(cell.BackgroundColor); //color.Value;
        }
        Command temp = new ColorChange(cellsChanged, prev, next);
        temp.execute();
        undo.Push(temp);
        redo.Clear();
    }

    public void RequestTextChange(Cell cellChanged, string next)
    {
        string prev = cellChanged.Text;
        Command temp = new textChange(cellChanged, prev, next);
        temp.execute();
        undo.Push(temp);
        redo.Clear();
    }

    private void EvaluateCellValue(Cell a)
    {
        ConcreteCell changeCell = (ConcreteCell)a;
        if (changeCell.Text.Length == 0)
        {
            changeCell.Value = changeCell.Text;
            return;
        }

        if (changeCell.Text[0] == '=')
        {
            ExpressionTree tree;
            try
            {
                tree = new ExpressionTree(changeCell.Text[1..]);
            }
            catch (Exception)
            {
                // Error in expression tree creation, must be operand missing since there is no other error that can occur here.
                changeCell.Value = "Operator Error";
                return;
            }

            // Remove no longer referenced.
            foreach (var item in changeCell.RefrencedTo.ToList())
            {
                if (!tree.GetVariables().Contains((char)(item.ColumnIndex + 'A') + (item.RowIndex + 1).ToString()))
                {
                    // Remove the reference from the refrencedTo list of changeCell
                    changeCell.RefrencedTo.Remove(item);

                    // Remove changeCell from the refrencedBy list of the item in the tree
                    if (item is ConcreteCell concreteItem)
                    {
                        concreteItem.ReferencedBy.Remove(changeCell);
                    }
                }
            }

            foreach (var item in tree.GetVariables())
            {
                try
                {
                    Cell? ab = this.GetCell(int.Parse(item.Substring(1)) - 1, item[0] - 'A') ?? null;
                    if (ab == null)
                    {
                        throw new Exception();
                    }

                    string test = ab.Value;
                    if (ab is ConcreteCell ex)
                    {
                        if (!ex.ReferencedBy.Contains(changeCell))
                        {
                            ex.ReferencedBy.Add(changeCell);
                            changeCell.RefrencedTo.Add(ex);
                        }
                    }

                    if (double.TryParse(test, out var value))
                    {
                        tree.SetVariable(item, value);
                    }
                }
                catch (Exception)
                {
                    // Error in finding the cell, cell reference must be wrong or some string is input that is not a cell.
                    changeCell.Value = "Cell Reference Error";
                    return;
                }
            }

            try
            {
                changeCell.Value = tree.Evaluate().ToString(CultureInfo.InvariantCulture);
            }
            catch (InvalidOperationException)
            {
                changeCell.Value = "##";
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
        if (row > this.RowCount || col > this.ColumnCount)
        {
            return null;
        }

        return this.Cells[row, col];
    }

    public string getRedoMessage()
    {
        if (redo.TryPeek(out Command output))
        {
            return "Redo " + output.message();
        }

        return "Redo";
    }
    public string getUndoMessage()
    {
        if (undo.TryPeek(out Command output))
        {
            return "Undo " + output.message();
        }

        return "Undo";
    }

    public bool emptyUndo()
    {
        if (undo.Count == 0)
        {
            return true;
        }

        return false;
    }
    public bool emptyRedo()
    {
        if (redo.Count == 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Cell that can be used to create an instance of Cell.
    /// </summary>
    private class ConcreteCell : Cell
    {
        #pragma warning disable SA1401
        /// <summary>
        /// List of Cells that reference this cell.
        /// </summary>
        internal readonly List<Cell> ReferencedBy = new List<Cell>();

        /// <summary>
        /// CList of Cells that this cell references.
        /// </summary>
        internal List<Cell> RefrencedTo = new List<Cell>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteCell"/> class.
        /// </summary>
        /// <param name="rowIndex">Integer that sets the row Index.</param>
        /// <param name="columnIndex">Integer that sets the column Index.</param>
        public ConcreteCell(int rowIndex, int columnIndex)
            : base(rowIndex, columnIndex)
        {
        }
        /*
        public event PropertyChangedEventHandler? ValuePropertyChanged = (sender, e) => { };
        
        protected virtual void OnValuePropertyChanged(string propertyName)
        {
            this.ValuePropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        */
        /// <summary>
        /// Gets or Sets value.
        /// </summary>
        public override string Value
        {
            get => this.StoredValue;

            set
            {
                if (this.StoredValue == value)
                {
                    return;
                }

                this.StoredValue = value;
                this.OnPropertyChanged(nameof(this.Value));
            }
        }
    }
}