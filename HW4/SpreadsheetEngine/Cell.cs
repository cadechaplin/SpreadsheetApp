namespace SpreadsheetEngine;

public abstract class Cell
{
    private readonly int _rowIndex;
    private readonly int columnIndex;
    
    public string cellValue;

    // Constructor to set the RowIndex value
    public Cell(int setRowIndex, int setColumnIndex)
    {
        _rowIndex = setRowIndex;
        columnIndex = setColumnIndex;

    }

    // Public read-only property to expose the RowIndex value
    public int RowIndex => _rowIndex;
    public int ColumnIndex => columnIndex;
}