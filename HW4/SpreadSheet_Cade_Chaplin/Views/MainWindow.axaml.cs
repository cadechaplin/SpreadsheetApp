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
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Avalonia.Automation.Peers;
using Avalonia.Layout;
using Avalonia.Media;


namespace SpreadSheet_Cade_Chaplin.Views
{

    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            
            InitializeComponent();
            //InitializeDataGrid();
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

        public void InitializeDataGrid()
        {
            if (myDataGrid == null)
            {
                throw new Exception("data grid null");
            }

            for (char column = 'A'; column <= 'Z'; column++)
            {
                // Create a DataGridTextColumn for each column
                var textColumn = new DataGridTextColumn
                {
                    Header = column.ToString(),
                    // Bind the Text property to the corresponding column data
                    
                };

                // Add the column to the DataGrid
                
                myDataGrid.Columns.Add(textColumn);
            }

        }






    }
    
}