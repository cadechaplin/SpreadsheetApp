using System;
using FibancciTextReader;
namespace HW3AvaloniaApp.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Numerics;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents the view model for the main window.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
#pragma warning disable CA1822 // Mark members as static
        public string CurrentText { get; set; }

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