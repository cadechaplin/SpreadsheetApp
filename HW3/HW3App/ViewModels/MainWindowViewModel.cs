using System;
using FibancciTextReader;

namespace HW3AvaloniaApp.ViewModels

{
    
    using System.ComponentModel;
    using System.IO;
    
    using System.Runtime.CompilerServices;
    using System.IO;
    using System.Reactive;
    using System.Reactive.Linq;
    using ReactiveUI;
    

    /// <summary>
    /// Represents the view model for the main window.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
#pragma warning disable CA1822 // Mark members as static
        public string CurrentText { get; set; }

        public Interaction<Unit, string?> AskForFileToLoad { get; }

        public bool SaveFileBoxOpen { get; set; }

        public string SaveFileName { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
             CurrentText = String.Empty;
             SaveFileName = String.Empty;
             SaveFileBoxOpen = false;
             AskForFileToLoad = new Interaction<Unit, string?>();

        }
        
        public void Fibonacci(object parameter)
        {
            // Event handling logic goes here
            
            if (parameter is string passedString)
            {
                FibonacciTextReader myText = new FibonacciTextReader(int.Parse(passedString));
                LoadText(myText);
            }
            else
            {
                return;
            }
        }

        public void SaveToFile()
        {
            

            // Get the current directory
            

            // Provide the file name and extension
            

            // Combine the current directory with the file name to create the full path
            string filePath = Path.Combine(Environment.CurrentDirectory, SaveFileName);

            // Write the text to the file
            File.WriteAllText(filePath, CurrentText);
            SaveToFileBoxToggle();




        }
        /*
        public void LoadFromFile()//should it ask for file name?
        {

            StreamReader fileToLoad = new StreamReader("File.txt");//change File.txt?
            LoadText(fileToLoad);


        }
        
        */
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

        public void SaveToFileBoxToggle()
        {
            SaveFileBoxOpen = !SaveFileBoxOpen;
            OnPropertyChanged(nameof(SaveFileBoxOpen));
            
        }

        /// <summary>
        /// Gets or sets the display value.
        /// </summary>
        /// <remarks>
        /// This property represents the display value used in the application.
        /// </remarks>
        

        private void LoadText(TextReader sr)
        {
            
            CurrentText = sr.ReadToEnd();
            OnPropertyChanged(nameof(CurrentText));


        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

#pragma warning restore CA1822 // Mark members as static
    }
}