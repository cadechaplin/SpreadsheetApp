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
                OnPropertyChanged(nameof(_Text));
            }
        }

    }

    public string Value{
        get { return _Value; }
        internal set { _Value = value; }
        /*
        set
        {
            if (value.Length < 1 || value == _Value)
            {
                return;
            }

            _Value = value;
            if (value[0] == '=')
            {
                //change cell to Value evaluation.TODO: change::
                _Value = Evalutate(value.Substring(1));
            }
            else
            {
                _Value = _Text;
            }


        }
        */
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


    private string Evalutate(string formula)
    {
        return formula;
    }


    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
//TODO: Move this ? Maybe shouldnt exist.
public class ConcreteCell : Cell
{
    public ConcreteCell(int rowIndex, int columnIndex, string text = "") : base(rowIndex, columnIndex)
    {
        Value = text;
    }
    
}