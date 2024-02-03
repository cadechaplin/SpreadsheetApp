namespace HW3AvaloniaApp.ViewModels;

/// <summary>
/// Gets or sets the display value.
/// </summary>
/// <remarks>
/// This property represents the display value used in the application.
/// </remarks>
public class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    /// <summary>
    /// Gets or sets the display value.
    /// </summary>
    /// /// <remarks>
    /// This property represents the display value used in the application.
    /// </remarks>
    public string Display { get; set; }

    public MainWindowViewModel()
    {
        Display = GetText();
    }

    private static string GetText() // this is your method
    {
        return "Line 1 \nLine 2 \nLine 3 \nLine 4 \n";
    }

#pragma warning restore CA1822 // Mark members as static
}