using Avalonia.Controls;

using SpreadSheet_Cade_Chaplin.ViewModels;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive.Linq;
using System;

namespace SpreadSheet_Cade_Chaplin.Views
{

    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            
            InitializeComponent();

            DataContextChanged += (sender, args) =>
            {
                if (DataContext is MainWindowViewModel viewModel)
                    viewModel.InitializeDataGrid(myDataGrid);
            };

        }

        






    }
    
}