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
            Console.Write("work");
            var dataGrid = this.FindControl<DataGrid>("SpreadsheetDataGrid");
            Console.Write("work");
            
            
            for (char column = 'A'; column <= 'Z'; column++)
            {
                var templateColumn = new DataGridTemplateColumn()
                {
                    Header = column.ToString(),
                    // Define the template for the column
                    CellTemplate = new FuncDataTemplate<string>((str, _) =>
                    {
                        // Create a TextBlock and bind its Text property to the current column's data
                        return new TextBlock
                        {
                            Text = str
                        };
                    }),
                    // Define the template for editing the column (optional)
                    CellEditingTemplate = new FuncDataTemplate<string>((str, _) =>
                    {
                        // Create a TextBox for editing and bind its Text property to the current column's data
                        return new TextBox
                        {
                            Text = str
                        };
                    }),
                };
                dataGrid.Columns.Add(templateColumn);
            }
            
            
        }
    }
}