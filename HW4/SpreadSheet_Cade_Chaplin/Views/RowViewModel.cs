using System.Collections.Generic;
using System.ComponentModel;
using ReactiveUI;
using SpreadSheet_Cade_Chaplin.ViewModels;
namespace Spreadsheet_GettingStarted.ViewModels;
public class RowViewModel : ViewModelBase
{
    public RowViewModel(List<CellViewModel> cells)
    {
        Cells = cells;
        foreach (var cell in Cells)
        {
            cell.PropertyChanged += CellOnPropertyChanged;
        }
        SelfReference = this;
    }
    public CellViewModel this[int index] => Cells[index];
    private void CellOnPropertyChanged(object? sender, PropertyChangedEventArgs
        args)
    {
        var styleImpactingProperties = new List<string>
        {
            nameof(CellViewModel.IsSelected),
            nameof(CellViewModel.CanEdit),
            nameof(CellViewModel.BackgroundColor),
        };
        if (styleImpactingProperties.Contains(args.PropertyName))
            FireChangedEvent();
    }
    public List<CellViewModel> Cells { get; set; }
    /// <summary>
    /// This property provides a way to notify the value converter that it needs to update
    /// </summary>
    public RowViewModel SelfReference { get; private set; }
    public void FireChangedEvent()
    {
        this.RaisePropertyChanged(nameof(SelfReference));
    }
}