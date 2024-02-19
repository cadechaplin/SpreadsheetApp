using Avalonia.Controls;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SpreadSheet_Cade_Chaplin.ViewModels;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Avalonia.Data;
using System.Reactive.Linq.ObservableImpl;
using System.Linq;
using System.Reactive.Linq;
using SpreadsheetEngine;
using System;
using System.Drawing.Printing;
using Avalonia.Controls.Templates;
using Avalonia.Controls.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace SpreadSheet_Cade_Chaplin.Views
{

    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>

    {
        private Cell myCell;
        private Spreadsheet _spreadsheet;
        private List<List<Cell>> _spreadsheetData;
        public MainWindow()
        {
            //Cell myCell;
            InitializeComponent();
            InitializeSpreadsheet();
            this.WhenAnyValue(x => x.DataContext)
                .Where(dataContext => dataContext != null)
                .Subscribe(dataContext =>
                {
                    if (dataContext is MainWindowViewModel)
                    {
                        InitializeDataGrid();
                    }
                });
            
        }

        private void InitializeSpreadsheet()
        {
            const int rowCount = 50;//Hard coded
            const int columnCount = 'Z' - 'A' + 1;

            _spreadsheet = new Spreadsheet( rowCount, columnCount);

            _spreadsheetData = new List<List<Cell>>(rowCount);
            foreach (var rowIndex in Enumerable.Range(0, rowCount))
            {
                var columns = new List<Cell>(columnCount);
                foreach (var columnIndex in Enumerable.Range(0, columnCount))
                {
                    columns.Add(_spreadsheet.Cells[rowIndex][columnIndex]);
                }

                _spreadsheetData.Add(columns);
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void InitializeDataGrid()
        {
            var dataGrid = this.FindControl<DataGrid>("SpreadsheetDataGrid");

            // Set the ItemsSource to the _spreadsheetData
            dataGrid.ItemsSource = _spreadsheetData;

            // Clear existing columns
            dataGrid.Columns.Clear();

            // Iterate over each column in the spreadsheet
            for (char column = 'A'; column <= 'Z'; column++)
            {
                // Create a DataGridTextColumn for each column
                var textColumn = new DataGridTextColumn
                {
                    Header = column.ToString(),
                    // Bind the Text property to the corresponding column data
                    
                };

                // Add the column to the DataGrid
                dataGrid.Columns.Add(textColumn);
            }
            dataGrid.IsReadOnly = false;
            
        }
    }
}