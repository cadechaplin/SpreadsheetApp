namespace SpreadsheetEngine;

public class SpreadSheet
{
    Cell[][] myCells;
    public SpreadSheet(int col, int row)
    {
        myCells = new Cell[col][];

        // Initialize each inner array separately
        for (int i = 0; i < col; i++)
        {
           
            myCells[i] = new Cell[row];
            for (int j = 0; j < row; j++)
            {
                // Pass row and column indices to the Cell constructor
                // TODO:possibly wrong approach??
                myCells[i][j] = new ConcreteCell(i, j);
            }
        }


    }
}