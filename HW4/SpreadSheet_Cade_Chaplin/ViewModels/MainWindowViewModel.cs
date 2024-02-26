namespace SpreadSheet_Cade_Chaplin.ViewModels;

using System.Linq;

using SpreadsheetEngine;

using System.Collections.Generic;
using ReactiveUI;

public class MainWindowViewModel : ViewModelBase
{
    private Spreadsheet _spreadsheet;
    private Cell[,] _spreadsheetData;
    public Cell[,]SpreadsheetData
    {
        get { return _spreadsheetData; }
        set { this.RaiseAndSetIfChanged(ref _spreadsheetData, value); }
    }
    public MainWindowViewModel()
    {
        InitializeDataGrid();
        
    }

    private void InitializeDataGrid()
    {
        const int rowCount = 50;
        const int columnCount = 'Z' - 'A' + 1;

        _spreadsheet = new Spreadsheet(row: rowCount, col: columnCount);
        _spreadsheetData = new Cell[rowCount, columnCount];

        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                _spreadsheetData[rowIndex, columnIndex] = _spreadsheet.Cells[columnIndex,rowIndex];
            }
        }
        
        
    }
#pragma warning disable CA1822 // Mark members as static
    
#pragma warning restore CA1822 // Mark members as static
}