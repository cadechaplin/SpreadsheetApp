// <copyright file="MainWindowViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Spreadsheet_GettingStarted.ViewModels;

namespace SpreadSheet_Cade_Chaplin.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Media;
using SpreadsheetEngine;

// disabling underscore warnings
#pragma warning disable SA1309

public class MainWindowViewModel : ViewModelBase
{
    private Spreadsheet _spreadSheetOb;
    private List<RowViewModel> _spreadsheetData;
    private readonly List<CellViewModel> _selectedCells = new();
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
        this.AskForAColor = new Interaction<Unit, uint?>();
        IsUndoReady = false;
        IsRedoReady = false;
        _uMessage = "Undo";
        _rMessage = "Redo";

    }

    public bool IsUndoReady
    {
        get { return _isUndoReady; }
        set
        {
            if (_isUndoReady != value)
            {
                this.RaiseAndSetIfChanged(ref _isUndoReady, value); // Notify property changed
            }
        }
    }
    public string undoMessage
    {
        get { return _uMessage; }
        set
        {
            if (_uMessage != value)
            {
                this.RaiseAndSetIfChanged(ref _uMessage, value); // Notify property changed
            }
        }
    }

    public void updateMessages()
    {
        undoMessage = _spreadSheetOb.getUndoMessage();
        redoMessage = _spreadSheetOb.getRedoMessage();
    }

    public string redoMessage
    {
        get { return _rMessage; }
        set
        {
            if (_rMessage != value)
            {
                this.RaiseAndSetIfChanged(ref _rMessage, value); // Notify property changed
            }
        }
    }
    public bool IsRedoReady
    {
        get { return _isRedoReady; }
        set
        {
            if (_isRedoReady != value)
            {
                this.RaiseAndSetIfChanged(ref _isRedoReady, value);
            }
        }
    }
    public void InitializeSpreadsheet(List<RowViewModel> sheetRows, Spreadsheet sheet)
    {
        _spreadsheetData = sheetRows;
        _spreadSheetOb = sheet;
    }

    public void SelectCell(int rowIndex, int columnIndex)
    {
        var clickedCell = GetCell(rowIndex, columnIndex);
        var shouldEditCell = clickedCell.IsSelected;
        ResetSelection();
// add the pressed cell back to the list
        _selectedCells.Add(clickedCell);
        clickedCell.IsSelected = true;
        if (shouldEditCell)
            clickedCell.CanEdit = true;
    }

    public void ToggleCellSelection(int rowIndex, int columnIndex)
    {
        var clickedCell = GetCell(rowIndex, columnIndex);
        if (false == clickedCell.IsSelected)
        {
            _selectedCells.Add(clickedCell);
            clickedCell.IsSelected = true;
        }
        else
        {
            _selectedCells.Remove(clickedCell);
            clickedCell.IsSelected = false;
        }
    }

    public void ResetSelection()
    {
        // clear current selection
        foreach (var cell in _selectedCells)
        {
            cell.IsSelected = false;
            cell.CanEdit = false;
        }

        _selectedCells.Clear();
    }

    public CellViewModel GetCell(int row, int col)
    {
        return _spreadsheetData[row][col];
    }
    public string GetCellText(int row, int col)
    {
        return _spreadsheetData[row][col].Text;
    }
    public void SetCellText(int row, int col, string val)
    {
        _spreadSheetOb.RequestTextChange(this.GetCell(row,col).Cell,val);
        this.IsRedoReady = false;
        this.IsUndoReady = true;
        this.updateMessages();
    }
    
    public Interaction<Unit, string?> AskForFileToLoad
    {
        get;
    }
    public async Task LoadFromFile()
    {
        // Wait for the user to select the file to load from.
        var filePath = await this.AskForFileToLoad.Handle(default);
        if (filePath == null)
        {
            return;
        }

        // If the user selected a file, create the stream reader and load the text.
        
    }
    public Interaction<Unit, uint?> AskForAColor
    {
        get;
    }
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
            _spreadSheetOb.RequestColorChange(cells,color.Value); // change to colo
            IsRedoReady = false;
            IsUndoReady = true;
            updateMessages();
        }
    }

    public void UndoCommand()
    {
        _spreadSheetOb.Undo();
        IsUndoReady = !_spreadSheetOb.emptyUndo();
        IsRedoReady = true;
        this.updateMessages();

    }

    public void RedoCommand()
    {
        _spreadSheetOb.Redo();
        IsRedoReady = !_spreadSheetOb.emptyRedo();
        IsUndoReady = true;
        this.updateMessages();
    }

}