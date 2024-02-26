namespace SpreadsheetEngine;
using System.ComponentModel;
public class Spreadsheet 
{
    public Cell[,] Cells;
    private int _ColumnCount;
    private int _RowCount;

    public int ColumnCount => _ColumnCount;
    

    public int RowCount => _RowCount;
    
    public event PropertyChangedEventHandler CellPropertyChanged = delegate { };
    public Spreadsheet(int row, int col)
    {
        Cells = new Cell[row,col];
        _ColumnCount = col;
        _RowCount = row;

        // Initialize each inner array separately
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                // Pass row and column indices to the Cell constructor
                Cells[i,j] = new ConcreteCell(i, j);
            }
        }

    
    }
    public Cell GetCell(int row, int col)
    {
        return this.Cells[row,col];
    }
    protected virtual void OnPropertyChanged(string propertyName)
    {
        CellPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}