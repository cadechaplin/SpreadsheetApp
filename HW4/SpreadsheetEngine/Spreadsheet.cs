namespace SpreadsheetEngine;
using System.ComponentModel;
public class Spreadsheet 
{
    public Cell[][] Cells;
    public event PropertyChangedEventHandler CellPropertyChanged;
    public Spreadsheet(int col, int row)
    {
        Cells = new Cell[col][];

        // Initialize each inner array separately
        for (int i = 0; i < col; i++)
        {
           
            Cells[i] = new Cell[row];
            for (int j = 0; j < row; j++)
            {
                // Pass row and column indices to the Cell constructor
                // TODO:possibly wrong approach??
                Cells[i][j] = new ConcreteCell(i, j);
            }
        }


    }
    protected virtual void OnPropertyChanged(string propertyName)
    {
        CellPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}