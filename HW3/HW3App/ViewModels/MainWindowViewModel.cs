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
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
             CurrentText = String.Empty;
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

        public void run()
        {
            LoadText(new StringReader(CurrentText));
            return;
        }

        /// <summary>
        /// Gets or sets the display value.
        /// </summary>
        /// <remarks>
        /// This property represents the display value used in the application.
        /// </remarks>
        public string CurrentText { get; set; }

        public void LoadText(TextReader sr)
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