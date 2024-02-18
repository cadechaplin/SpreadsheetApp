using Avalonia.Controls;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SpreadSheet_Cade_Chaplin.ViewModels;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Avalonia.Data;
using System.Reactive.Linq.ObservableImpl;
using System.Linq;
using System.Reactive.Linq;

namespace SpreadSheet_Cade_Chaplin.Views
{

    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>

    {
        public MainWindow()
        {

            InitializeComponent();
            this.WhenAnyValue(x => x.DataContext)
                .Where(dataContext => dataContext != null);

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void InitializeDataGrid()
        {
            
        }
    }
}