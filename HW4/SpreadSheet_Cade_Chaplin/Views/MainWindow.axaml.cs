// <copyright file="MainWindow.axaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls.Converters;
using Avalonia.Input;
using Avalonia.Platform.Storage;
using ReactiveUI;
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
    private List<RowViewModel> RowsView;
    private CellViewModel[][] Rows { get; }
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        this.WhenActivated(d =>
            d(this.ViewModel!.AskForFileToLoad.RegisterHandler(this.DoOpenFile)));
        this.WhenActivated(d =>
            d(this.ViewModel!.AskForAColor.RegisterHandler(this.PickColor)));
        this._spreadsheet = new Spreadsheet(50, 'Z' - 'A' + 1);
        var rowCount = this._spreadsheet.RowCount;
        var columnCount = this._spreadsheet.ColumnCount;
        //this.Rows = new Cell[columnCount][rowCount]();
        
        this.Rows = Enumerable.Range(0, rowCount)
            .Select(row => Enumerable.Range(0, columnCount)
                .Select(column => new CellViewModel(this._spreadsheet.Cells[row, column])).ToArray())
            .ToArray();
        //this.Rows[0][0] = null;
        this.RowsView = new List<RowViewModel>();
        foreach (var col in Rows)
        {
            RowsView.Add(new RowViewModel(col.ToList()));
        }
        
        this.InitializeComponent();
        
        
        this.DataContextChanged += (sender, args) =>
        {
            if (this.DataContext is MainWindowViewModel viewModel)
            {
                viewModel.InitializeSpreadsheet(this.RowsView);
                this.InitializeDataGrid(this.MyDataGrid,viewModel);
                //viewModel.InitializeSpreadsheet(this.MyDataGrid);
            }
        };
        
    }
    public void InitializeDataGrid(DataGrid dataGrid, MainWindowViewModel viewModel)
    {
        
        //this._myGrid = dataGrid;
        
        

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
                CellStyleClasses = { "SpreadsheetCellClass" },
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
    
    private async Task DoOpenFile(InteractionContext<Unit, string?> interaction)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        // List of filtered types
        var fileType = new FilePickerFileType("txt");
        var fileTypes = new List<FilePickerFileType>();
        fileTypes.Add(fileType);

        // Start async operation to open the dialog.
        var filePath = await topLevel.StorageProvider.OpenFilePickerAsync(new
            FilePickerOpenOptions
            {
                Title = "Open Text File",
                AllowMultiple = false,
                FileTypeFilter = fileTypes,
            });
        interaction.SetOutput(filePath.Count == 1 ? filePath[0].Path.AbsolutePath :
            null);
    }
    private async Task PickColor(InteractionContext<Unit, uint?> interaction)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var colorPicker = new ColorPicker();
        colorPicker.DataContext = interaction.Input;
        interaction.SetOutput(5);
        RowsView[0][0].BackgroundColor += 152;
        
        //var colorChoice = await dia;

        // Show the color picker dialog as a modal dialog
        //var color = await colorPicker.
        // Get top level from the current control. Alternatively, you can use Window reference instead.

    }


    
}
