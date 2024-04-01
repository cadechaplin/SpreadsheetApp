// <copyright file="MainWindow.axaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Avalonia.Input;
using Spreadsheet_GettingStarted.ViewModels;

namespace SpreadSheet_Cade_Chaplin.Views;

using Avalonia.ReactiveUI;
#pragma warning disable SA1135
// Contradictory warnings
using ViewModels;
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

/// <summary>
/// Joins a first name and a last name together into a single string.
/// </summary>
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    private readonly Spreadsheet _spreadsheet;
    private bool _isInitialized;
    private DataGrid _myGrid;
    private CellViewModel[][] Rows { get; }
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        this._spreadsheet = new Spreadsheet(50, 'Z' - 'A' + 1);
        var rowCount = this._spreadsheet.RowCount;
        var columnCount = this._spreadsheet.ColumnCount;
        //this.Rows = new Cell[columnCount][rowCount]();
        
        this.Rows = Enumerable.Range(0, rowCount)
            .Select(row => Enumerable.Range(0, columnCount)
                .Select(column => new CellViewModel(this._spreadsheet.Cells[row, column])).ToArray())
            .ToArray();
        //this.Rows[0][0] = null;
        
        this.InitializeComponent();
        
        
        this.DataContextChanged += (sender, args) =>
        {
            if (this.DataContext is MainWindowViewModel viewModel)
            {
                this.InitializeDataGrid(this.MyDataGrid,viewModel);
                //viewModel.InitializeSpreadsheet(this.MyDataGrid);
            }
        };
        
    }
    public void InitializeDataGrid(DataGrid dataGrid, MainWindowViewModel viewModel)
    {
        
        //this._myGrid = dataGrid;
        
        List<RowViewModel> RowsView = new List<RowViewModel>();
        foreach (var col in Rows)
        {
            RowsView.Add(new RowViewModel(col.ToList()));
        }

        // initialize A to Z columns headers since these are indexed this is not a behavior supported by default
        var columnCount = 'Z' - 'A' + 1;
        foreach (var columnIndex in Enumerable.Range(0, columnCount))
        {
            // for each column we will define the header text and
            // the binding to use
            var columnHeader = (char)('A' + columnIndex);
            var columnTemplate = new DataGridTemplateColumn
            {
                Header = columnHeader,
                CellTemplate = new
                    FuncDataTemplate<RowViewModel>((value, namescope) =>
                        new TextBlock
                        {
                            [!TextBlock.TextProperty] =
                                new Binding($"[{columnIndex}].Value"),//TODO changed
                            TextAlignment = TextAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Center,
                            Padding = Thickness.Parse("5,0,5,0")
                        }),
                CellEditingTemplate = new
                    FuncDataTemplate<RowViewModel>((value, namescope) =>
                        new TextBox()
                    ),
            };
            dataGrid.Columns.Add(columnTemplate);
        }

        dataGrid.ItemsSource = RowsView;
        dataGrid.LoadingRow += (sender, args) => { args.Row.Header = (args.Row.GetIndex() + 1).ToString(); };
        this._isInitialized = true;
        dataGrid.PreparingCellForEdit += (sender, args) =>
        {
            if (args.EditingElement is not TextBox textInput) return;
            var rowIndex = args.Row.GetIndex();
            var columnIndex = args.Column.DisplayIndex;
            textInput.Text = viewModel.GetCellText(rowIndex,
                columnIndex);
        };
        dataGrid.CellEditEnding += (sender, args) =>
        {
            if (args.EditingElement is not TextBox textInput) return;
            var rowIndex = args.Row.GetIndex();
            var columnIndex = args.Column.DisplayIndex;
            viewModel.SetCellText(rowIndex, columnIndex,
                textInput.Text);
        };
        dataGrid.CellPointerPressed += (sender, args) =>
        {
// get the pressed cell
            var rowIndex = args.Row.GetIndex();
            var columnIndex = args.Column.DisplayIndex;
// are we selected multiple cells
            var multipleSelection =
                args.PointerPressedEventArgs.KeyModifiers != KeyModifiers.None;
            if (multipleSelection == false)
            {
                viewModel.SelectCell(rowIndex, columnIndex);
            }
            else
            {
                viewModel.ToggleCellSelection(rowIndex, columnIndex);
            }
        };
        dataGrid.BeginningEdit += (sender, args) =>
        {
// get the pressed cell
            var rowIndex = args.Row.GetIndex();
            var columnIndex = args.Column.DisplayIndex;
            var cell = viewModel.GetCell(rowIndex, columnIndex);
            if (false == cell.CanEdit)
            {
                args.Cancel = true;
            }
            else
            {
                viewModel.ResetSelection();
            }
        };
    }
    
}
