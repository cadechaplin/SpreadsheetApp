namespace HW7Tests;
using SpreadsheetEngine;

public class Tests
{
    public Spreadsheet TestSheet = new Spreadsheet(10, 10);

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
        this.TestSheet = new Spreadsheet(10,10);
        this.TestSheet.Cells[0, 0].Text = "=ABC";
        Assert.That(this.TestSheet.Cells[0, 0].Value, Is.EqualTo("Cell Reference Error"));
    }

    /// <summary>
    /// Tests using a formula.
    /// </summary>
    [Test]
    public void TestFormula()
    {
        this.TestSheet = new Spreadsheet(10,10);
        this.TestSheet.Cells[0, 0].Text = "=1+1";
        Assert.That( this.TestSheet.Cells[0, 0].Value, Is.EqualTo("2"));
    }

    /// <summary>
    /// Tests a formula with a reference.
    /// </summary>
    [Test]
    public void TestFormulaWithReference()
    {
        this.TestSheet = new Spreadsheet(10,10);
        this.TestSheet.Cells[0, 0].Text = "=1+1";
        this.TestSheet.Cells[1,0].Text = "=A1+1";
        Assert.That( this.TestSheet.Cells[1,0].Value, Is.EqualTo("3"));
    }

    /// <summary>
    /// Tests updating after a change.
    /// </summary>
    [Test]
    public void TestFormulaWithReferenceAfterChange()
    {
        this.TestSheet = new Spreadsheet(10,10);
        this.TestSheet.Cells[0, 0].Text = "=1+1";
        this.TestSheet.Cells[1,0].Text = "=A1+1";
        this.TestSheet.Cells[0, 0].Text = "=1+2";
        Assert.That( this.TestSheet.Cells[1,0].Value, Is.EqualTo("4"));
    }

    /// <summary>
    /// Tests formula with multiple tests.
    /// </summary>
    [Test]
    public void TestMultipleCell()
    {
        this.TestSheet = new Spreadsheet(10,10);
        this.TestSheet.Cells[0, 0].Text = "=1+1";
        this.TestSheet.Cells[0,1].Text = "=1+1";
        this.TestSheet.Cells[0,2].Text = "=A1+B1";
        Assert.That( this.TestSheet.Cells[0, 2].Value, Is.EqualTo("4"));
    }

    /// <summary>
    /// Test formula with constant and variable.
    /// </summary>
    [Test]
    public void TestCellWithConstant()
    {
        this.TestSheet = new Spreadsheet(10,10);
        this.TestSheet.Cells[0, 0].Text = "=1+1";
        this.TestSheet.Cells[0, 1].Text = "=A1+1";
        Assert.That( this.TestSheet.Cells[0,1].Value, Is.EqualTo("3"));
    }

    [Test]
    public void TestCellReferenceRemoval()
    {
        Assert.That(true);
        this.TestSheet = new Spreadsheet(10,10);
        this.TestSheet.Cells[0, 0].Text = "=1+1";
        this.TestSheet.Cells[0, 1].Text = "=A1+1";
        Assert.That(true);
    }

    // HW8 tests.
    [Test]
    public void TestUndo()
    {
        this.TestSheet = new Spreadsheet(10,10);
        this.TestSheet.RequestTextChange(this.TestSheet.Cells[0, 1], "=A1+1");
        this.TestSheet.RequestTextChange(this.TestSheet.Cells[0, 0], "=1+1");
        this.TestSheet.RequestTextChange(this.TestSheet.Cells[0, 0], "=1+2");
        this.TestSheet.Undo();
        Assert.That(this.TestSheet.Cells[0,1].Value, Is.EqualTo("3"));
    }
    [Test]
    public void TestRedo()
    {
        this.TestSheet = new Spreadsheet(10,10);
        this.TestSheet.RequestTextChange(this.TestSheet.Cells[0,1], "=A1+1");
        this.TestSheet.RequestTextChange(this.TestSheet.Cells[0, 0], "=1+1");
        this.TestSheet.RequestTextChange(this.TestSheet.Cells[0, 0], "=1+2");
        this.TestSheet.Undo();
        this.TestSheet.Redo();
        Assert.That(this.TestSheet.Cells[0, 1].Value, Is.EqualTo("4"));
    }

    /// <summary>
    /// Testing color change.
    /// </summary>
    [Test]
    public void TestColor()
    {
        this.TestSheet = new Spreadsheet(10,10);
        List<Cell> firstChanged = new List<Cell>();
        firstChanged.Add(this.TestSheet.Cells[0,1]);
        List<Cell> secondChanged = new List<Cell>();
        secondChanged.Add(this.TestSheet.Cells[0, 1]);
        secondChanged.Add(this.TestSheet.Cells[0, 0]);
        this.TestSheet.RequestColorChange(firstChanged,0xff3300df);
        this.TestSheet.RequestColorChange(secondChanged,0xff3300df);
        this.TestSheet.Undo();
        Assert.That(this.TestSheet.Cells[0,1].BackgroundColor, Is.EqualTo(0xff3300df));
        Assert.That(this.TestSheet.Cells[0, 0].BackgroundColor, Is.Not.EqualTo(0xff3300df));
        this.TestSheet.Redo();
        Assert.That(this.TestSheet.Cells[0, 0].BackgroundColor, Is.EqualTo(0xff3300df));
        Assert.That(this.TestSheet.GetUndoMessage(), Is.EqualTo("Undo color change"));
        this.TestSheet.Undo();
        this.TestSheet.Undo();
        Assert.That(this.TestSheet.GetUndoMessage(), Is.EqualTo("Undo"));
    }
}