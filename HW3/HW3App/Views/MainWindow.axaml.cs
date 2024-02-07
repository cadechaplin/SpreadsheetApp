// <copyright file="MainWindow.axaml.cs" company="CptS 321 Instructor">
// Copyright (c) CptS 321 Instructor. All rights reserved.
// </copyright>
namespace HW3AvaloniaApp.Views;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using HW3AvaloniaApp.ViewModels;
using ReactiveUI;
/// <summary>
/// This class handles all necessary UI events to communicate with the view model
///and sub windows.
/// </summary>
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
/// <summary>
/// Initializes a new instance of the <see cref="MainWindow"/> class.
/// </summary>
public MainWindow()
{
    InitializeComponent();
    DataContext = new MainWindowViewModel();
    this.WhenActivated(d =>
        d(ViewModel!.AskForFileToLoad.RegisterHandler(DoOpenFile)));
    // TODO: add code for saving
}
/// <summary>
/// Opens a dialog to select a file which will be used to load content.
/// </summary>
/// <param name="interaction">Defines the Input/Output necessary for this
///workflow to complete successfully.</param>
/// <returns>A <see cref="Task"/> representing the result of the asynchronous
///operation.</returns>
private async Task DoOpenFile(InteractionContext<Unit, string?> interaction)
{
var fileDialog = new OpenFileDialog
{
AllowMultiple = false,
};
var txtFiler = new FileDialogFilter
{
Extensions = { ".txt" },
};
var fileDialogFilters = new List<FileDialogFilter>
{
txtFiler,
};
fileDialog.Filters = fileDialogFilters;
var filePath = await fileDialog.ShowAsync(this);
interaction.SetOutput(filePath is { Length: 1 } ? filePath[0] : null);
}
/// <summary>
/// Opens a dialog to select a file in which content will be saved.
/// </summary>
/// <param name="interaction">Defines the Input/Output necessary for this
///workflow to complete successfully.</param>
/// <returns>A <see cref="Task"/> representing the result of the asynchronous
///operation.</returns>
private async Task DoSaveFile(InteractionContext<Unit, string?> interaction)
{
// TODO: your code goes here.
}
}
