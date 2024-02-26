namespace SpreadsheetEngine;
using System.ComponentModel;
public class Spreadsheet 
{
    public Cell[,] Cells;
    private int _ColumnCount;
    private int _RowCount;

    public int ColumnCount => _ColumnCount;
    

    public int RowCount => _RowCount;
    
    
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
                Cells[i, j].PropertyChanged += OnCellPropertyChanged;
            }
        }

    
    }
    public Cell GetCell(int row, int col)
    {
        if (row > this._RowCount || col > ColumnCount)
        {
            return null;
        }

        return this.Cells[row,col];
    }

    public void EvaluateCellValue(Cell ChangeCell)
    {
        if ((ChangeCell.Text.Length < 3))
        {
            ChangeCell.Value = ChangeCell.Text;
            return;
        }

        if (ChangeCell.Text[0] == '=')
        {
            Cell refrenced = GetCell(int.Parse(ChangeCell.Text.Substring(2)) - 1,ChangeCell.Text[1] - 'A');
            ChangeCell.Value = refrenced.Value;
        }
        else
        {
            ChangeCell.Value = ChangeCell.Text;
        }


    }

    protected virtual void OnCellPropertyChanged(object sender,
        PropertyChangedEventArgs e)
    {

        EvaluateCellValue((Cell)sender);
        
    }
}