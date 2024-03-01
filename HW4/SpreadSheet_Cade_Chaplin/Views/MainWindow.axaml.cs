// <copyright file="MainWindow.axaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadSheet_Cade_Chaplin.Views;

using Avalonia.ReactiveUI;
#pragma warning disable SA1135
// Contradictory warnings
using ViewModels;

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
        this.DataContextChanged += (sender, args) =>
        {
            if (this.DataContext is MainWindowViewModel viewModel)
            {
                viewModel.InitializeDataGrid(this.MyDataGrid);
            }
        };
    }
}
