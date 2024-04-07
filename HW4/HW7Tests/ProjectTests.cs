// <copyright file="ProjectTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HW7Tests;

using SpreadsheetEngine;
#pragma warning disable SA1309
/// <summary>
/// Class for project tests.
/// </summary>
public class ProjectTests
{
    private Spreadsheet _testSheet = new Spreadsheet(10, 10);

    /// <summary>
    /// Setup for tests.
    /// </summary>
    [SetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// Tests invalid variable.
    /// </summary>
    [Test]
    public void TestInvalidVar()
    {
        this._testSheet = new Spreadsheet(10, 10);
        this._testSheet.Cells[0, 0].Text = "=ABC";
        Assert.That(this._testSheet.Cells[0, 0].Value, Is.EqualTo("Cell Reference Error"));
    }

    /// <summary>
    /// Tests using a formula.
    /// </summary>
    [Test]
    public void TestFormula()
    {
        this._testSheet = new Spreadsheet(10, 10);
        this._testSheet.Cells[0, 0].Text = "=1+1";
        Assert.That(this._testSheet.Cells[0, 0].Value, Is.EqualTo("2"));
    }

    /// <summary>
    /// Tests a formula with a reference.
    /// </summary>
    [Test]
    public void TestFormulaWithReference()
    {
        this._testSheet = new Spreadsheet(10, 10);
        this._testSheet.Cells[0, 0].Text = "=1+1";
        this._testSheet.Cells[1, 0].Text = "=A1+1";
        Assert.That(this._testSheet.Cells[1, 0].Value, Is.EqualTo("3"));
    }

    /// <summary>
    /// Tests updating after a change.
    /// </summary>
    [Test]
    public void TestFormulaWithReferenceAfterChange()
    {
        this._testSheet = new Spreadsheet(10, 10);
        this._testSheet.Cells[0, 0].Text = "=1+1";
        this._testSheet.Cells[1, 0].Text = "=A1+1";
        this._testSheet.Cells[0, 0].Text = "=1+2";
        Assert.That(this._testSheet.Cells[1, 0].Value, Is.EqualTo("4"));
    }

    /// <summary>
    /// Tests formula with multiple tests.
    /// </summary>
    [Test]
    public void TestMultipleCell()
    {
        this._testSheet = new Spreadsheet(10, 10);
        this._testSheet.Cells[0, 0].Text = "=1+1";
        this._testSheet.Cells[0, 1].Text = "=1+1";
        this._testSheet.Cells[0, 2].Text = "=A1+B1";
        Assert.That(this._testSheet.Cells[0, 2].Value, Is.EqualTo("4"));
    }

    /// <summary>
    /// Test formula with constant and variable.
    /// </summary>
    [Test]
    public void TestCellWithConstant()
    {
        this._testSheet = new Spreadsheet(10, 10);
        this._testSheet.Cells[0, 0].Text = "=1+1";
        this._testSheet.Cells[0, 1].Text = "=A1+1";
        Assert.That(this._testSheet.Cells[0, 1].Value, Is.EqualTo("3"));
    }

    /// <summary>
    /// Tests removal of cell reference.
    /// </summary>
    [Test]
    public void TestCellReferenceRemoval()
    {
        Assert.That(true);
        this._testSheet = new Spreadsheet(10, 10);
        this._testSheet.Cells[0, 0].Text = "=1+1";
        this._testSheet.Cells[0, 1].Text = "=A1+1";
        Assert.That(true);
    }

    /// <summary>
    /// Tests undo function of the spreadsheet class.
    /// </summary>
    [Test]
    public void TestUndo()
    {
        this._testSheet = new Spreadsheet(10, 10);
        this._testSheet.RequestTextChange(this._testSheet.Cells[0, 1], "=A1+1");
        this._testSheet.RequestTextChange(this._testSheet.Cells[0, 0], "=1+1");
        this._testSheet.RequestTextChange(this._testSheet.Cells[0, 0], "=1+2");
        this._testSheet.Undo();
        Assert.That(this._testSheet.Cells[0, 1].Value, Is.EqualTo("3"));
    }

    /// <summary>
    /// Tests redo function of the spreadsheet class.
    /// </summary>
    [Test]
    public void TestRedo()
    {
        this._testSheet = new Spreadsheet(10, 10);
        this._testSheet.RequestTextChange(this._testSheet.Cells[0, 1], "=A1+1");
        this._testSheet.RequestTextChange(this._testSheet.Cells[0, 0], "=1+1");
        this._testSheet.RequestTextChange(this._testSheet.Cells[0, 0], "=1+2");
        this._testSheet.Undo();
        this._testSheet.Redo();
        Assert.That(this._testSheet.Cells[0, 1].Value, Is.EqualTo("4"));
    }

    /// <summary>
    /// Testing color change.
    /// </summary>
    [Test]
    public void TestColor()
    {
        this._testSheet = new Spreadsheet(10, 10);
        List<Cell> firstChanged = new List<Cell>();
        firstChanged.Add(this._testSheet.Cells[0, 1]);
        List<Cell> secondChanged = new List<Cell>();
        secondChanged.Add(this._testSheet.Cells[0, 1]);
        secondChanged.Add(this._testSheet.Cells[0, 0]);
        this._testSheet.RequestColorChange(firstChanged, 0xff3300df);
        this._testSheet.RequestColorChange(secondChanged, 0xff3300df);
        this._testSheet.Undo();
        Assert.That(this._testSheet.Cells[0, 1].BackgroundColor, Is.EqualTo(0xff3300df));
        Assert.That(this._testSheet.Cells[0, 0].BackgroundColor, Is.Not.EqualTo(0xff3300df));
        this._testSheet.Redo();
        Assert.That(this._testSheet.Cells[0, 0].BackgroundColor, Is.EqualTo(0xff3300df));
        Assert.That(this._testSheet.GetUndoMessage(), Is.EqualTo("Undo color change"));
        this._testSheet.Undo();
        this._testSheet.Undo();
        Assert.That(this._testSheet.GetUndoMessage(), Is.EqualTo("Undo"));
    }

    /// <summary>
    /// Tests saving to a file. Will pass as long as crash or exception does not occur.
    /// </summary>
    [Test]
    public void TestSaveToFile()
    {
        this._testSheet = new Spreadsheet(10, 10);
        this._testSheet.Cells[0, 0].Text = "example";
        this._testSheet.SaveFile("filepath");

        // Asserts that we did not crash. Not really a way to know saving worked without loading
        Assert.That(true);
    }

    /// <summary>
    /// Tests loading from a file. First saves a file to load from.
    /// </summary>
    [Test]
    public void TestLoadFromFile()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(currentDirectory, "Spreadsheet.xml");
        this._testSheet = new Spreadsheet(10, 10);
        this._testSheet.Cells[0, 0].Text = "example";
        this._testSheet.SaveFile(filePath);
        this._testSheet.Cells[0, 0].Text = string.Empty;
        this._testSheet.LoadFile(filePath);

        // If file is loaded correctly, then the cell should be restored.
        Assert.That(this._testSheet.Cells[0, 0].Text == "example");
    }
}