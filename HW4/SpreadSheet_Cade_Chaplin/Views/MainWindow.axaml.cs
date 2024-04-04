// <copyright file="MainWindow.axaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadSheet_Cade_Chaplin.Views;

#pragma warning disable SA1135
// Contradictory warnings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Spreadsheet_GettingStarted.ViewModels;
using SpreadsheetEngine;
using ViewModels;
#pragma warning disable SA1309
/// <summary>
/// Joins a first name and a last name together into a single string.
/// </summary>
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    private readonly Spreadsheet _spreadsheet;
    private readonly List<RowViewModel> _rowsView;
    private bool _isInitialized;

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
        this.Rows = Enumerable.Range(0, rowCount)
            .Select(row => Enumerable.Range(0, columnCount)
                .Select(column => new CellViewModel(this._spreadsheet.Cells[row, column])).ToArray())
            .ToArray();
        this._rowsView = new List<RowViewModel>();
        foreach (var col in this.Rows)
        {
            this._rowsView.Add(new RowViewModel(col.ToList()));
        }

        this.InitializeComponent();
        this.DataContextChanged += (sender, args) =>
        {
            if (this.DataContext is MainWindowViewModel viewModel)
            {
                this.InitializeDataGrid(this.MyDataGrid, viewModel);
                viewModel.InitializeSpreadsheet(this._rowsView, this._spreadsheet);
            }
        };
    }

    private CellViewModel[][] Rows { get; }

    /// <summary>
    /// Initializes the datagrid.
    /// </summary>
    /// <param name="dataGrid">dataGrid object.</param>
    /// <param name="viewModel">viewModel object.</param>
    public void InitializeDataGrid(DataGrid dataGrid, MainWindowViewModel viewModel)
    {
        if (this._isInitialized)
        {
            return;
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
                CellStyleClasses = { "SpreadsheetCellClass" },
                CellTemplate = new
                    FuncDataTemplate<RowViewModel>((value, namescope) =>
                        new TextBlock
                        {
                            [!TextBlock.TextProperty] =
                                new Binding($"[{columnIndex}].Value"),
                            TextAlignment = TextAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Center,
                            Padding = Thickness.Parse("5,0,5,0"),
                        }),
                CellEditingTemplate = new
                    FuncDataTemplate<RowViewModel>((value, namescope) =>
                        new TextBox()),
            };
            dataGrid.Columns.Add(columnTemplate);
        }

        dataGrid.ItemsSource = this._rowsView;
        dataGrid.LoadingRow += (sender, args) => { args.Row.Header = (args.Row.GetIndex() + 1).ToString(); };
        this._isInitialized = true;
        dataGrid.PreparingCellForEdit += (sender, args) =>
        {
            if (args.EditingElement is not TextBox textInput)
            {
                return;
            }

            var rowIndex = args.Row.GetIndex();
            var columnIndex = args.Column.DisplayIndex;
            textInput.Text = viewModel.GetCellText(rowIndex, columnIndex);
        };
        dataGrid.CellEditEnding += (sender, args) =>
        {
            if (args.EditingElement is not TextBox textInput)
            {
                return;
            }

            var rowIndex = args.Row.GetIndex();
            var columnIndex = args.Column.DisplayIndex;
            viewModel.SetCellText(rowIndex, columnIndex, textInput.Text);
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
            if (cell.CanEdit == false)
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

    [Obsolete("Obsolete")]
    private async Task PickColor(InteractionContext<Unit, uint?> interaction)
    {
        var colorPicker = new ColorPicker();
        var stackPanel = new StackPanel();
        stackPanel.Children.Add(colorPicker);
        Window dialog = new Window
        {
            Content = stackPanel,
            Title = "Choose background color",
            Width = 300,
            Height = 200,
        };
        await dialog.ShowDialog(this);
        interaction.SetOutput(colorPicker.Color.ToUint32());
    }
}
