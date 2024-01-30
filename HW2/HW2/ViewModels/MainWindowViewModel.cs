namespace HW2.ViewModels;
using myfunctions;
public class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public MainWindowViewModel()
    {
        Greeting = RunDistinctIntegers();
    }

    private string RunDistinctIntegers() // this is your method
    {
        HW2Prog goober = new HW2Prog();
        
        return goober.run();
    }

    public string Greeting { get; set; }
}