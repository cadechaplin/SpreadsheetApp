// <copyright file="MainWindowViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
    private List<RowViewModel> _spreadsheetData;
    private readonly List<CellViewModel> _selectedCells = new();
    private Stack<Command> redo;
    private Stack<Command> undo;
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {
        redo = new Stack<Command>();
        undo = new Stack<Command>();
        this.AskForFileToLoad = new Interaction<Unit, string?>();
        this.AskForAColor = new Interaction<Unit, uint?>();
    }

    public void InitializeSpreadsheet(List<RowViewModel> sheet)
    {
        _spreadsheetData = sheet;
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
        string prev = _spreadsheetData[row][col].Text;
        _spreadsheetData[row][col].Text = val;
        string next = val;
        undo.Push(new textChange(_spreadsheetData[row][col],prev,next));
        redo.Clear();
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

        List<uint> prevColors = new List<uint>();
        
        foreach (CellViewModel cell in this._selectedCells)
        {
            prevColors.Add(cell.BackgroundColor); //color.Value;
        }
        // Wait for the user to select the file to load from.
        var color = await this.AskForAColor.Handle(default);
        if (color == null)
        {
            return;
        }
        else
        {
            
            foreach (CellViewModel cell in this._selectedCells)
            {
                cell.BackgroundColor = 0xff3300df; //color.Value;
            }

            color = 0xff3300df;
            undo.Push(new ColorChange(this._selectedCells, prevColors, color.Value));
            redo.Clear();
        }

        // If the user selected a file, create the stream reader and load the text.
        
    }

    public void UndoCommand()
    {
        if (undo.Count > 0)
        {
            Command temp = undo.Pop();
            temp.unexecute();
            redo.Push(temp);
        }

    }

    public void RedoCommand()
    {
        if (redo.Count > 0)
        {
            Command temp = redo.Pop();
            temp.execute();
            undo.Push(temp);
        }
        
    }

}