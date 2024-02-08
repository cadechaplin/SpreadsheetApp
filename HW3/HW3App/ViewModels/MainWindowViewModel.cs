// <copyright file="MainWindow.axaml.cs" company="CptS 321 Instructor">
// Copyright (c) CptS 321 Instructor. All rights reserved.
// </copyright>
namespace HW3AvaloniaApp.ViewModels;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using FibancciTextReaderClass;
public class MainWindowViewModel : ViewModelBase
{
    private string _currentText;
    public string CurrentText
    {
        get => _currentText;
        set => this.RaiseAndSetIfChanged(ref _currentText,  value);
    }

    public MainWindowViewModel()
    {
// Create an interaction between the view model and the view for the file to be loaded:
        AskForFileToLoad = new Interaction<Unit, string?>();
        
// Similarly to load, there is a need to create an interaction for saving into a file:
// TODO: Your code goes here.
        AskForFileToSave = new Interaction<Unit, string?>();
    }

    /// <summary>
    /// This method will be executed when the user wants to load content from a file.
    /// </summary>
    public async void LoadFromFile()
    {
        // Wait for the user to select the file to load from.
        var filePath = await AskForFileToLoad.Handle(default);
        if (filePath == null) return;

        // If the user selected a file, create the stream reader and load the text.
        var textReader = new StreamReader(filePath);
        LoadText(textReader);
        textReader.Close();
    }

    public async void SaveToFile()
    {
// TODO: Implement this method.
// Wait for the user to select the file to load from.
        var filePath = await AskForFileToSave.Handle(default);
        File.WriteAllText(filePath, CurrentText);


    }
    private void LoadText(TextReader sr)
    {
            
        CurrentText = sr.ReadToEnd();
        


    }

    public void Fibonacci(int count)
    {
        FibonacciTextReader myFib = new FibonacciTextReader(count);
        LoadText(myFib);

    }

    public Interaction<Unit, string?> AskForFileToLoad { get; }
    public Interaction<Unit, string?> AskForFileToSave { get; }
    
// other code...
}