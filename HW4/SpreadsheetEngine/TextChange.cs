namespace SpreadsheetEngine;

public class textChange : Command
{
    private Cell affectedCell;
    private string prev;
    private string changed;
    
    static string messageText = "text change";

    public textChange(Cell cell, string prevVal, string changeToVal)
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
    public string message()
    {
        return messageText;
    }
}