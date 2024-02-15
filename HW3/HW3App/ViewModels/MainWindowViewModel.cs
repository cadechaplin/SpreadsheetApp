namespace HW3AvaloniaApp.ViewModels;

using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;

/// <summary>
/// Represents the main view model for the MainWindow.
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    // ReSharper disable once InconsistentNaming
    private string currentTextData = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// This method will be executed when the user wants to load content from a file.
    /// </summary>
    public MainWindowViewModel()
    {
        // Create an interaction between the view model and the view for the file to be loaded:
        this.AskForFileToLoad = new Interaction<Unit, string?>();

        // Similarly to load, there is a need to create an interaction for saving into a file.
        this.AskForFileToSave = new Interaction<Unit, string?>();
    }

    /// <summary>
    /// Gets or sets the currentTextData which stores the string in the displayed box. Sets the same string but also makes sure it is
    /// updated on the display.
    /// </summary>
    public string CurrentText
    {
        get => this.currentTextData;
        set => this.RaiseAndSetIfChanged(ref this.currentTextData,  value);
    }

    /// <summary>
    /// Gets the interaction to ask for a file to load.
    /// </summary>
    public Interaction<Unit, string?> AskForFileToLoad
    {
        get;
    }

    /// <summary>
    /// Gets the interaction to ask for a file to save.
    /// </summary>
    public Interaction<Unit, string?> AskForFileToSave
    {
        get;
    }

    /// <summary>
    /// This method will be executed when the user wants to load content from a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the operation or if an error occurs while loading, no action is taken.
    /// </remarks>
    /// /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task LoadFromFile()
    {
        // Wait for the user to select the file to load from.
        var filePath = await this.AskForFileToLoad.Handle(default);
        if (filePath == null)
        {
            return;
        }

        // If the user selected a file, create the stream reader and load the text.
        var textReader = new StreamReader(filePath);
        this.LoadText(textReader);
        textReader.Close();
    }

    /// <summary>
    /// This method will be executed when the user wants to save content to a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the operation or if an error occurs while saving, no action is taken.
    /// </remarks>
    /// /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task SaveToFile()
    {
        // TODO: Implement this method.
        // Wait for the user to select the file to load from.
        var filePath = await this.AskForFileToSave.Handle(default);
        if (filePath == null)
        {
            return;
        }

        await File.WriteAllTextAsync(filePath, this.CurrentText);
    }

    /// <summary>
    /// Gets the interaction to ask for a file to load.
    /// </summary>
    /// /// <param name="count">The number of Fibonacci numbers to generate.</param>
    public void Fibonacci(int count)
    {
        var myFib = new FibonacciTextReader(count);
        this.LoadText(myFib);
    }

    private void LoadText(TextReader sr)
    {
        this.CurrentText = sr.ReadToEnd();
    }
}