using System.ComponentModel;

namespace SpreadsheetEngine;

public abstract class Cell : INotifyPropertyChanged
{
    private readonly int _rowIndex;
    private readonly int columnIndex;
    protected string cellText;
    protected string cellValue;

    // Constructor to set the RowIndex value
    public Cell(int setRowIndex, int setColumnIndex)
    {
        _rowIndex = setRowIndex;
        columnIndex = setColumnIndex;

    }

    protected string cellTextProperty
    {
        get
        {
            return cellText;
        }

        set
        {
            if (cellText != value)
            {
                cellText = value;
                if (value[0] == '=')
                {
                    //change cell to Value evaluation.
                    
                }

                OnPropertyChanged(nameof(cellTextProperty));
            }
        }

    }

    // Public read-only property to expose the RowIndex value
    public int RowIndex => _rowIndex;
    public int ColumnIndex => columnIndex;
    
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
//TODO: Move this ? Maybe shouldnt exist.
public class ConcreteCell : Cell
{
    public ConcreteCell(int rowIndex, int columnIndex) : base(rowIndex, columnIndex)
    {
    }

}