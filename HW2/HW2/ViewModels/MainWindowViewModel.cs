namespace HW2.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public MainWindowViewModel()
    {
        Greeting = RunDistinctIntegers();
    }

    private string RunDistinctIntegers() // this is your method
    {
        return "Hello world";
    }

    public string Greeting { get; set; }
}