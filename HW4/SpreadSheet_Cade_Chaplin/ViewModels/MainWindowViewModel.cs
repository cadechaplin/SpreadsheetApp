using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Media;

namespace SpreadSheet_Cade_Chaplin.ViewModels;
using Avalonia.Controls;

using SpreadSheet_Cade_Chaplin.ViewModels;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive.Linq;
using System;
using SpreadsheetEngine;
using System.Linq;

using ReactiveUI;

public class MainWindowViewModel : ViewModelBase
{
    private Spreadsheet _spreadsheet;
    
    
    private bool _isInitialized = false;
    public Cell[][] Rows { get; }
    
    public MainWindowViewModel()
    {
        _spreadsheet = new Spreadsheet('Z' - 'A' + 1,50);
        int rowCount = _spreadsheet.RowCount; 
        int columnCount = _spreadsheet.ColumnCount;
        Rows = Enumerable.Range(0, rowCount)
            .Select(row => Enumerable.Range(0, columnCount)
                .Select(column => _spreadsheet.Cells[row,column]).ToArray())
            .ToArray();
    }

    public void InitializeDataGrid(DataGrid dataGrid)
    {
        if (_isInitialized) return;
        

        // initialize A to Z columns headers since these are indexed this is not a behavior supported by default
        var columnCount = 'Z' - 'A' + 1;
        foreach (var columnIndex in Enumerable.Range(0, columnCount))
        {
            // for each column we will define the header text and
            // the binding to use
            var columnHeader = (char) ('A' + columnIndex);
            var columnTemplate = new DataGridTemplateColumn
            {
                Header = columnHeader,
                CellTemplate = new FuncDataTemplate<IEnumerable<Cell>>((value, namescope) =>
                    new TextBlock
                    {
                        [!TextBlock.TextProperty] = new Binding($"[{columnIndex}].Value"),
                        TextAlignment = TextAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Padding = Thickness.Parse("5,0,5,0")
                    }),
                CellEditingTemplate = new FuncDataTemplate<IEnumerable<Cell>>((value, namescope) =>
                    new TextBox
                    {
                        [!TextBox.TextProperty] = new Binding($"[{columnIndex}].Text")
                    })
            };
            dataGrid.Columns.Add(columnTemplate);
        }

        dataGrid.ItemsSource = Rows;
        dataGrid.LoadingRow += (sender, args) => { args.Row.Header = (args.Row.GetIndex() + 1).ToString(); };

        _isInitialized = true;
    }


}