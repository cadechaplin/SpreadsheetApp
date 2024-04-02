

namespace SpreadsheetEngine;

public class ColorChange : Command
{
    private List<Cell> affectedCells;
    private List<uint> prev;
    private uint next;

    public ColorChange(List<Cell> cells, List<uint> prev, uint next)
    {
        this.prev = prev;
        this.next = next;
        affectedCells = new List<Cell>(cells);
    }

    public void execute()
    {
        foreach (Cell item in affectedCells)
        {
            item.BackgroundColor = next;
        }
    }

    public void unexecute()
    {
        for (int i = 0; i < affectedCells.Count; i++)
        {
            affectedCells[i].BackgroundColor = prev[i];
        }
    }
}