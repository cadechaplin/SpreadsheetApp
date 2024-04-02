using Spreadsheet_GettingStarted.ViewModels;

namespace SpreadsheetEngine;

public class textChange : Command
{
    private CellViewModel affectedCell;
    private string prev;
    private string changed;

    public textChange(CellViewModel cell, string prevVal, string changeToVal)
    {
        affectedCell = cell;
        prev = prevVal;
        changed = changeToVal;
    }

    public void execute()
    {
        this.affectedCell.Text = changed;
    }

    public void unexecute()
    {
        this.affectedCell.Text = prev;
    }
}