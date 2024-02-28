using System.ComponentModel;

namespace SpreadsheetEngine;

public abstract class Cell : INotifyPropertyChanged
{
    private readonly int _rowIndex;
    private readonly int columnIndex;
    protected string _Text;
    protected string _Value;

    public string Text
    {
        get { return _Text; }

        set
        {
            if (_Text != value)
            {
                _Text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

    }

    public string Value{
        get { return _Value; }
        internal set
        {
            _Value = value;
            OnValuePropertyChanged(nameof(Value));
        }
    }

    // Public read-only property to expose the RowIndex value
    public int RowIndex => _rowIndex;
    public int ColumnIndex => columnIndex;

    // Constructor to set the RowIndex value
    public Cell(int setRowIndex, int setColumnIndex)
    {
        _rowIndex = setRowIndex;
        columnIndex = setColumnIndex;
    }

    


    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public event PropertyChangedEventHandler ValuePropertyChanged = delegate { };
    protected virtual void OnValuePropertyChanged(string propertyName)
    {
        ValuePropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
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