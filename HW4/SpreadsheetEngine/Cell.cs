using System.ComponentModel;

namespace SpreadsheetEngine;

public abstract class Cell : INotifyPropertyChanged
{
    private readonly int _rowIndex;
    private readonly int columnIndex;
    
    private string _Text;
    private string _Value;

    protected string Text
    {
        get { return Text; }

        set
        {
            if (_Text != value)
            {
                _Text = value;
                if (value[0] == '=')
                {
                    //change cell to Value evaluation.TODO: change::
                    _Text = "Value"; 
                }


                OnPropertyChanged(nameof(_Text));
            }
        }

    }

    protected string Value;
    // Public read-only property to expose the RowIndex value
    public int RowIndex => _rowIndex;
    public int ColumnIndex => columnIndex;

    // Constructor to set the RowIndex value
    public Cell(int setRowIndex, int setColumnIndex)
    {
        _rowIndex = setRowIndex;
        columnIndex = setColumnIndex;

    }

    

    
    
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
//TODO: Move this ? Maybe shouldnt exist.
public class ConcreteCell : Cell
{
    public ConcreteCell(int rowIndex, int columnIndex, string text = "Uhh") : base(rowIndex, columnIndex)
    {
        Text = text;
    }

}