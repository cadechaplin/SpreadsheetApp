namespace SpreadsheetEngine;
using System.ComponentModel;
public class Spreadsheet 
{
    public Cell[,] Cells;
    private int _ColumnCount;
    private int _RowCount;

    public int ColumnCount => _ColumnCount;
    

    public int RowCount => _RowCount;
    
    public event PropertyChangedEventHandler CellPropertyChanged;
    public Spreadsheet(int col, int row)
    {
        Cells = new Cell[col,row];

        // Initialize each inner array separately
        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
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