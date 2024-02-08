namespace HW3.ViewModels;

using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;

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
    }
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
    private void LoadText(TextReader sr)
    {
            
        CurrentText = sr.ReadToEnd();
        //OnPropertyChanged(nameof(currentText));


    }

    

    

    public async void SaveToFile()
    {
// TODO: Implement this method.
    }
    public Interaction<Unit, string?> AskForFileToLoad { get; }

}