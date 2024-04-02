using System.Collections.Generic;
using System.Runtime.Versioning;
using Spreadsheet_GettingStarted.ViewModels;

namespace SpreadsheetEngine;

public class ColorChange : Command
{
    private List<CellViewModel> affectedCells;
    private List<uint> prev;
    private uint next;

    public ColorChange(List<CellViewModel> cells, List<uint> prev, uint next)
    {
        this.prev = prev;
        this.next = next;
        affectedCells = new List<CellViewModel>(cells);
    }

    public void execute()
    {
        foreach (CellViewModel item in affectedCells)
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