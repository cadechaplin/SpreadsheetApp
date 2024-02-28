using DynamicData.Binding;
using ReactiveUI;

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

/// <summary>
/// Joins a first name and a last name together into a single string.
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    private bool _isInitialized;
    private Spreadsheet spreadsheet;
    private DataGrid mygrid;
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {
        spreadsheet = new Spreadsheet(50, 'Z' - 'A' + 1);
        var rowCount = spreadsheet.RowCount;
        var columnCount = spreadsheet.ColumnCount;
        this.Rows = Enumerable.Range(0, rowCount)
            .Select(row => Enumerable.Range(0, columnCount)
                .Select(column => spreadsheet.Cells[row, column]).ToArray())
            .ToArray();
    }

    private Cell[][] Rows { get; }

    /// <summary>
    /// Initializes the data grid with cells.
    /// </summary>
    /// <param name="dataGrid">Data grid that will be initialized.</param>
    public void InitializeDataGrid(DataGrid dataGrid)
    {
        if (this._isInitialized)
        {
            return;
        }

        mygrid = dataGrid;

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
                CellTemplate = new FuncDataTemplate<IEnumerable<Cell>>((value, namescope) =>
                    new TextBlock
                    {
                        [!TextBlock.TextProperty] = new Binding($"[{columnIndex}].Value"),
                        TextAlignment = TextAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Padding = Thickness.Parse("5,0,5,0"),
                    }),
                CellEditingTemplate = new FuncDataTemplate<IEnumerable<Cell>>((value, namescope) =>
                    new TextBox
                    {
                        [!TextBox.TextProperty] = new Binding($"[{columnIndex}].Text"),
                    }),
            };
            dataGrid.Columns.Add(columnTemplate);
        }

        dataGrid.ItemsSource = this.Rows;
        dataGrid.LoadingRow += (sender, args) => { args.Row.Header = (args.Row.GetIndex() + 1).ToString(); };
        this._isInitialized = true;
    }

    /// <summary>
    /// Runs a demo.
    /// </summary>
    public void RunDemo()
    {
        Random random = new Random();
        for (int i = 0; i < 50; i++)
        {

            this.spreadsheet.Cells[random.Next(0, 49), random.Next(0, 26)].Text = "Hello world!!";
        }

        for (int i = 0; i < 50; i++)
        {
            this.spreadsheet.Cells[i, 1].Text = "This is Cell B" + (this.Rows[i][1].RowIndex + 1);
        }

        for (int i = 0; i < 50; i++)
        {
            this.spreadsheet.Cells[i, 0].Text = "=B" + (i + 1).ToString();
        }
    }
}