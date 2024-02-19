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

namespace SpreadSheet_Cade_Chaplin.Views
{

    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>

    {
        private Cell myCell;
        public MainWindow()
        {
            //Cell myCell;
            InitializeComponent();
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

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void InitializeDataGrid()
        {
            Console.Write("work");
            var dataGrid = this.FindControl<DataGrid>("SpreadsheetDataGrid");
            Console.Write("work");
            SpreadSheet mySheet = new SpreadSheet(10,15);
            
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
                    })
                    
                    
                };
                
                dataGrid.Columns.Add(templateColumn);
                
            }
            
            
        }
    }
}