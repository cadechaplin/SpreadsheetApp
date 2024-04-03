// <copyright file="ColorChange.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine;
#pragma warning disable SA1309
/// <summary>
/// Command for color change.
/// </summary>
public class ColorChange : ICommand
{
    private static readonly string MessageText = "color change";
    private readonly List<Cell> _affectedCells;
    private readonly List<uint> _prev;
    private readonly uint _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorChange"/> class.
    /// </summary>
    /// <param name="cells">Cells that are changed.</param>
    /// <param name="prev">List of colors for all previous cells.</param>
    /// <param name="next">Color they are all changing to.</param>
    public ColorChange(List<Cell> cells, List<uint> prev, uint next)
    {
        this._prev = prev;
        this._next = next;
        this._affectedCells = new List<Cell>(cells);
    }

    /// <inheritdoc/>
    public void Execute()
    {
        foreach (Cell item in this._affectedCells)
        {
            item.BackgroundColor = this._next;
        }
    }

    /// <inheritdoc/>
    public void Unexecute()
    {
        for (int i = 0; i < this._affectedCells.Count; i++)
        {
            this._affectedCells[i].BackgroundColor = this._prev[i];
        }
    }

    /// <inheritdoc/>
    public string Message()
    {
        return MessageText;
    }
}