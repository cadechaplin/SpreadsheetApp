namespace SpreadsheetEngine;

public interface Command
{
    public void execute();
    public void unexecute();
}