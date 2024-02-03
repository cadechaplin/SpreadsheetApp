namespace HW3AvaloniaApp.ViewModels
{
    /// <summary>
    /// Represents the view model for the main window.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
#pragma warning disable CA1822 // Mark members as static
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.Display = GetText();
        }

        /// <summary>
        /// Gets or sets the display value.
        /// </summary>
        /// <remarks>
        /// This property represents the display value used in the application.
        /// </remarks>
        public string Display { get; set; }

        private static string GetText()
        {
            return "Line 1 \nLine 2 \nLine 3 \nLine 4 \n";
        }

#pragma warning restore CA1822 // Mark members as static
    }
}