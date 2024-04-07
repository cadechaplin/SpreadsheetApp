// <copyright file="MainWindowViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadSheet_Cade_Chaplin.ViewModels;

using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Spreadsheet_GettingStarted.ViewModels;
using SpreadsheetEngine;

// disabling underscore warnings
#pragma warning disable SA1309
/// <summary>
/// Main Window class.
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    private readonly List<CellViewModel> _selectedCells = new();
    private Spreadsheet _spreadSheetOb;
    private List<RowViewModel> _spreadsheetData;
    private bool _isUndoReady;
    private bool _isRedoReady;
    private string _uMessage;
    private string _rMessage;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {
        this.AskForFileToLoad = new Interaction<Unit, string?>();
        this.AskForFileToSave = new Interaction<Unit, string?>();
        this.AskForAColor = new Interaction<Unit, uint?>();
        this.IsUndoReady = false;
        this.IsRedoReady = false;
        this._uMessage = "Undo";
        this._rMessage = "Redo";
    }

    /// <summary>
    /// Gets or sets a value indicating whether undo can be preformed.
    /// </summary>
    public bool IsUndoReady
    {
        get
        {
            return this._isUndoReady;
        }

        set
        {
            if (this._isUndoReady != value)
            {
                this.RaiseAndSetIfChanged(ref this._isUndoReady, value); // Notify property changed
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether redo can be preformed.
    /// </summary>
    public bool IsRedoReady
    {
        get
        {
            return this._isRedoReady;
        }

        set
        {
            if (this._isRedoReady != value)
            {
                this.RaiseAndSetIfChanged(ref this._isRedoReady, value);
            }
        }
    }

    /// <summary>
    /// Gets or sets message to display in undo menu header.
    /// </summary>
    public string UndoMessage
    {
        get => this._uMessage;

        set
        {
            if (this._uMessage != value)
            {
                this.RaiseAndSetIfChanged(ref this._uMessage, value); // Notify property changed
            }
        }
    }

    /// <summary>
    /// Gets or sets message to display in redo menu header.
    /// </summary>
    public string RedoMessage
    {
        get => this._rMessage;

        set
        {
            if (this._rMessage != value)
            {
                this.RaiseAndSetIfChanged(ref this._rMessage, value); // Notify property changed
            }
        }
    }

    /// <summary>
    /// Gets Interaction for asking a file to load.
    /// </summary>
    public Interaction<Unit, string?> AskForFileToLoad
    {
        get;
    }

    /// <summary>
    /// Gets Interaction for asking a file to save.
    /// </summary>
    public Interaction<Unit, string?> AskForFileToSave
    {
        get;
    }

    /// <summary>
    /// Gets for asking for a color.
    /// </summary>
    /// <returns> uint describing color.</returns>
    public Interaction<Unit, uint?> AskForAColor
    {
        get;
    }

    /// <summary>
    /// Initializes spreadsheet objects with objects already created.
    /// </summary>
    /// <param name="sheetRows"> Used for RowViewModel.</param>
    /// <param name="sheet"> Used for spreadsheet object.</param>
    public void InitializeSpreadsheet(List<RowViewModel> sheetRows, Spreadsheet sheet)
    {
        this._spreadsheetData = sheetRows;
        this._spreadSheetOb = sheet;
    }

    /// <summary>
    /// Method for adding cell to selected cell list.
    /// </summary>
    /// <param name="rowIndex"> Row of cell added.</param>
    /// <param name="columnIndex"> column of cell added.</param>
    public void SelectCell(int rowIndex, int columnIndex)
    {
        var clickedCell = this.GetCell(rowIndex, columnIndex);
        var shouldEditCell = clickedCell.IsSelected;
        this.ResetSelection();
        this._selectedCells.Add(clickedCell);
        clickedCell.IsSelected = true;
        if (shouldEditCell)
        {
            clickedCell.CanEdit = true;
        }
    }

    /// <summary>
    /// Method for toggling cell selection.
    /// </summary>
    /// <param name="rowIndex"> Row of cell added.</param>
    /// <param name="columnIndex"> column of cell added.</param>
    public void ToggleCellSelection(int rowIndex, int columnIndex)
    {
        var clickedCell = this.GetCell(rowIndex, columnIndex);
        if (clickedCell.IsSelected == false)
        {
            this._selectedCells.Add(clickedCell);
            clickedCell.IsSelected = true;
        }
        else
        {
            this._selectedCells.Remove(clickedCell);
            clickedCell.IsSelected = false;
        }
    }

    /// <summary>
    /// Clears currently selected cells.
    /// </summary>
    public void ResetSelection()
    {
        // clear current selection
        foreach (var cell in this._selectedCells)
        {
            cell.IsSelected = false;
            cell.CanEdit = false;
        }

        this._selectedCells.Clear();
    }

    /// <summary>
    /// Gets cell at row and col.
    /// </summary>
    /// <returns>Cell at index.</returns>
    /// <param name="row"> Row of cell added.</param>
    /// <param name="col"> column of cell added.</param>
    public CellViewModel GetCell(int row, int col)
    {
        return this._spreadsheetData[row][col];
    }

    /// <summary>
    /// Gets cell text at row and col.
    /// </summary>
    /// <returns>Cell at index.</returns>
    /// <param name="row"> Row of cell added.</param>
    /// <param name="col"> column of cell added.</param>
    public string GetCellText(int row, int col)
    {
        return this._spreadsheetData[row][col].Text;
    }

    /// <summary>
    /// Gets cell at row and col.
    /// </summary>
    /// <param name="row"> Row of cell added.</param>
    /// <param name="col"> column of cell added.</param>
    /// <param name="val"> string that text should be set to.</param>
    public void SetCellText(int row, int col, string val)
    {
        this._spreadSheetOb.RequestTextChange(this.GetCell(row, col).Cell, val);
        this.IsRedoReady = false;
        this.IsUndoReady = true;
        this.UpdateMessages();
    }

    /// <summary>
    /// Task for asking a file to load.
    /// </summary>
    /// <returns> Nothing.</returns>
    public async Task LoadFromFile()
    {
        // Wait for the user to select the file to load from.
        var filePath = await this.AskForFileToLoad.Handle(default);
        if (filePath == null)
        {
            return;
        }

        this._spreadSheetOb.LoadFile(filePath);
        this.UpdateMessages();
    }

    /// <summary>
    /// Task for asking a file to load.
    /// </summary>
    public void NewDoc()
    {
        this._spreadSheetOb.ClearSpreadSheet();
        this.UpdateMessages();
    }

    /// <summary>
    /// This method will be executed when the user wants to save content to a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the operation or if an error occurs while saving, no action is taken.
    /// </remarks>
    /// /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task SaveToFile()
    {
        // Wait for the user to select the file to load from.
        var filePath = await this.AskForFileToSave.Handle(default);
        if (filePath == null)
        {
            return;
        }

        this._spreadSheetOb.SaveFile(filePath);
        this.UpdateMessages();
    }

    /// <summary>
    /// Task for asking for a color.
    /// </summary>
    /// <returns> Nothing.</returns>
    public async Task ColorPicker()
    {
        if (this._selectedCells.Count == 0)
        {
            return;
        }

        // Wait for the user to select the file to load from.
        var color = await this.AskForAColor.Handle(default);
        if (color != null)
        {
            List<Cell> cells = this._selectedCells.Select(vm => vm.Cell).ToList();
            this._spreadSheetOb.RequestColorChange(cells, color.Value); // change to colo
            this.IsRedoReady = false;
            this.IsUndoReady = true;
            this.UpdateMessages();
        }
    }

    /// <summary>
    /// Undoes the last command given to the spreadsheet.
    /// </summary>
    public void UndoCommand()
    {
        this._spreadSheetOb.Undo();
        this.IsUndoReady = !this._spreadSheetOb.EmptyUndo();
        this.IsRedoReady = true;
        this.UpdateMessages();
    }

    /// <summary>
    /// Redoes the last command undone to the spreadsheet.
    /// </summary>
    public void RedoCommand()
    {
        this._spreadSheetOb.Redo();
        this.IsRedoReady = !this._spreadSheetOb.EmptyRedo();
        this.IsUndoReady = true;
        this.UpdateMessages();
    }

    /// <summary>
    /// Gets or sets message to display in undo menu header.
    /// </summary>
    private void UpdateMessages()
    {
        this.UndoMessage = this._spreadSheetOb.GetUndoMessage();
        this.RedoMessage = this._spreadSheetOb.GetRedoMessage();
    }
}