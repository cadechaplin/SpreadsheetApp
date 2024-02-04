
using System.ComponentModel;

namespace HW3AvaloniaApp.ViewModels
{
    using System.IO;
    using System.ComponentModel;
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
            this.Display = GetText();
        }

        private void LoadText(TextReader sr)
        {
            this.Display = sr.ReadToEnd();
        }

        public void Fibonacci(object parameter)
        {
            // Event handling logic goes here
            this.Display = parameter.ToString();
        }

        /// <summary>
        /// Gets or sets the display value.
        /// </summary>
        /// <remarks>
        /// This property represents the display value used in the application.
        /// </remarks>
        private string _display; 
        public string Display
        {
            get { return _display; }
            set
            {
                if (_display != value)
                {
                    _display = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string GetText()
        {
            return "nothing";
        }
        

#pragma warning restore CA1822 // Mark members as static
    }
}