// <copyright file="MainWindowViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
    private List<RowViewModel> _spreadsheetData = null;
    private readonly List<CellViewModel> _selectedCells = new();
    public Spreadsheet mySheet;
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {
        mySheet = new Spreadsheet(50, 'Z' - 'A' + 1);
    }

    public void InitializeSpreadsheet()
    {
        
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
        throw new Exception("Not implemented");
    }
    public string GetCellText(int row, int col)
    {
        throw new Exception("Not implemented");
    }
    public string SetCellText(int row, int col, string val)
    {
        throw new Exception("Not implemented");
    }

}