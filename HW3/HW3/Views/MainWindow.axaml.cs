using Avalonia.Controls;

namespace HW3.Views;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using HW3.ViewModels;
using ReactiveUI;
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        this.WhenActivated(d =>
            d(ViewModel!.AskForFileToLoad.RegisterHandler(DoOpenFile)));
    }
    private async Task DoOpenFile(InteractionContext<Unit, string?> interaction)
    {
        var fileDialog = new OpenFileDialog
        {
            AllowMultiple = false,
        };
        var txtFiler = new FileDialogFilter
        {
            Extensions = { "txt" },
        };
        var fileDialogFilters = new List<FileDialogFilter>
        {
            txtFiler,
        };
        fileDialog.Filters = fileDialogFilters;
        var filePath = await fileDialog.ShowAsync(this);
        interaction.SetOutput(filePath is { Length: 1 } ? filePath[0] : null);
    }
    private async Task DoSaveFile(InteractionContext<Unit, string?> interaction)
    {
// TODO: your code goes here.
    }
}