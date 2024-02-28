namespace SpreadSheet_Cade_Chaplin.Views;

using Avalonia.ReactiveUI;
using SpreadSheet_Cade_Chaplin.ViewModels;

/// <summary>
/// Joins a first name and a last name together into a single string.
/// </summary>
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        this.InitializeComponent();
        DataContext = new MainWindowViewModel();//works without. 
        this.DataContextChanged += (sender, args) =>
        {
            if (this.DataContext is MainWindowViewModel viewModel)
            {
                viewModel.InitializeDataGrid(this.MyDataGrid);
            }
        };
    }
}
