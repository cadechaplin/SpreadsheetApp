// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine;

using System.ComponentModel;
using System.Globalization;
using System.Xml;

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
    private Stack<ICommand> _redo;
    private Stack<ICommand> _undo;
#pragma warning restore SA1401

    /// <summary>
    /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
    /// </summary>
    /// <param name="row"> Amount of rows to initiate.</param>
    /// <param name="col"> Amount of columns to initiate.</param>
    public Spreadsheet(int row, int col)
    {
        this._redo = new Stack<ICommand>();
        this._undo = new Stack<ICommand>();
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
    /// Undo on spreadsheet.
    /// </summary>
    public void Undo()
    {
        if (this._undo.Count == 0)
        {
            return;
        }

        var temp = this._undo.Pop();
        temp.Unexecute();
        this._redo.Push(temp);
    }

    /// <summary>
    /// Redo on spreadsheet.
    /// </summary>
    public void Redo()
    {
        // Should be impossible for count to be 0, since I disable the button is the stack is empty.
        if (this._redo.Count == 0)
        {
            return;
        }

        var temp = this._redo.Pop();
        temp.Execute();
        this._undo.Push(temp);
    }

    /// <summary>
    /// Color change for a list of cells. Empty lists should be
    /// handled before this method is called so that a command
    /// that changes no cells is not called.
    /// </summary>
    /// /// <param name="cellsChanged">List of cells to be changed.</param>
    /// /// <param name="next">Color to change to.</param>
    public void RequestColorChange(List<Cell> cellsChanged, uint next)
    {
        List<uint> prev = new List<uint>();
        foreach (Cell cell in cellsChanged)
        {
            prev.Add(cell.BackgroundColor);
        }

        ICommand temp = new ColorChange(cellsChanged, prev, next);
        temp.Execute();
        this._undo.Push(temp);
        this._redo.Clear();
    }

    /// <summary>
    /// Creates text change and executes command.
    /// </summary>
    /// /// <param name="cellChanged">Cell to be changed.</param>
    /// /// <param name="next">Color to change to.</param>
    public void RequestTextChange(Cell cellChanged, string next)
    {
        string prev = cellChanged.Text;
        ICommand temp = new TextChange(cellChanged, prev, next);
        temp.Execute();
        this._undo.Push(temp);
        this._redo.Clear();
    }

    /// <summary>
    /// Save to a file.
    /// </summary>
    /// <param name="filePath"> Path in which file needs to be loaded to.</param>
    public void LoadFile(string filePath)
    {
        // First clear spreadsheet.
        this.ClearSpreadSheet();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);

        XmlNodeList? cellNodes = xmlDoc.SelectNodes("//Cells/Cell");

        if (cellNodes == null)
        {
            return;
        }

        foreach (XmlNode cellNode in cellNodes)
        {
            int row = int.Parse(cellNode.SelectSingleNode("Row").InnerText);
            int column = int.Parse(cellNode.SelectSingleNode("Column").InnerText);
            string text = cellNode.SelectSingleNode("Text").InnerText;
            string backgroundColor = cellNode.SelectSingleNode("BackgroundColor").InnerText;
            try
            {
                this.Cells[row, column].Text = text;
                this.Cells[row, column].BackgroundColor = uint.Parse(backgroundColor);
            }
            catch (Exception e)
            {
                this.ClearSpreadSheet();
                Console.WriteLine(e);
                throw;
            }
        }
    }

    /// <summary>
    /// Clears all the values and colors from the spreadsheet.
    /// </summary>
    public void ClearSpreadSheet()
    {
        foreach (var cell in this.Cells)
        {
            cell.Text = string.Empty;
            cell.BackgroundColor = 0;
        }

        this._redo = new Stack<ICommand>();
        this._undo = new Stack<ICommand>();
    }

    /// <summary>
    /// Load from a file.
    /// </summary>
    /// <param name="filePath"> Path in which file needs to be loaded from.</param>
    public void SaveFile(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
        xmlDoc.AppendChild(xmlDeclaration);

        XmlNode spreadsheetNode = xmlDoc.CreateElement("Spreadsheet");

        XmlNode cellsNode = xmlDoc.CreateElement("Cells");
        spreadsheetNode.AppendChild(cellsNode);

        for (int i = 0; i < this.Cells.GetLength(0); i++)
        {
            for (int j = 0; j < this.Cells.GetLength(1); j++)
            {
                Cell cell = this.Cells[i, j];
                XmlNode cellNode = cell.ToXmlNode(xmlDoc);
                cellsNode.AppendChild(cellNode);
            }
        }

        spreadsheetNode.AppendChild(cellsNode);
        xmlDoc.AppendChild(spreadsheetNode);
        xmlDoc.Save(filePath);
    }

    /// <summary>
    /// Gets message from command at top of redo stack.
    /// </summary>
    /// <returns>Message from command at top of stack, if nothing in stack returns just redo.</returns>>
    public string GetRedoMessage()
    {
        if (this._redo.TryPeek(out ICommand? output))
        {
            return "Redo " + output.Message();
        }

        return "Redo";
    }

    /// <summary>
    /// Gets message from command at top of undo stack.
    /// </summary>
    /// <returns>Message from command at top of stack, if nothing in stack returns just Undo.</returns>>
    public string GetUndoMessage()
    {
        if (this._undo.TryPeek(out ICommand? output))
        {
            return "Undo " + output.Message();
        }

        return "Undo";
    }

    /// <summary>
    /// Check if undo stack is empty.
    /// </summary>
    /// <returns>True if stack is empty, otherwise false..</returns>>
    public bool EmptyUndo()
    {
        if (this._undo.Count == 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Check if redo stack is empty.
    /// </summary>
    /// <returns>True if stack is empty, otherwise false..</returns>>
    public bool EmptyRedo()
    {
        if (this._redo.Count == 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Gets the column count.
    /// </summary>
    /// <param name="sender"> Integer to use for indexing the Cell row.</param>
    /// <param name="e"> Integer to use for indexing the Cell .</param>
    protected virtual void OnCellPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is Cell)
        {
            this.EvaluateCellValue((Cell)sender);
        }

        if (sender is ConcreteCell cell)
        {
            foreach (var item in cell.ReferencedBy)
            {
                this.EvaluateCellValue(item);
            }
        }
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
                    // Remove the reference from the referencedTo list of changeCell
                    changeCell.RefrencedTo.Remove(item);

                    // Remove changeCell from the referencedBy list of the item in the tree
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
                        throw new ArgumentOutOfRangeException();
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
                catch (ArgumentOutOfRangeException)
                {
                    changeCell.Value = "!(bad reference)";
                    return;
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