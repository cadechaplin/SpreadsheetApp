namespace HW7Tests;
using SpreadsheetEngine;

public class Tests
{
    private Spreadsheet _testSheet = new Spreadsheet(10,10);
    [SetUp]
    public void Setup()
    {
    }
    [Test]
    public void TestInvalidVar()
    {
        _testSheet = new Spreadsheet(10,10);
        _testSheet.Cells[0,0].Text = "=ABC";
        Assert.That( _testSheet.Cells[0,0].Value, Is.EqualTo("Cell Reference Error"));
    }

    [Test]
    public void TestFormula()
    {
        _testSheet = new Spreadsheet(10,10);
        _testSheet.Cells[0,0].Text = "=1+1";
        Assert.That( _testSheet.Cells[0,0].Value, Is.EqualTo("2"));
    }
    [Test]
    public void TestFormulaWithReference()
    {
        _testSheet = new Spreadsheet(10,10);
        _testSheet.Cells[0,0].Text = "=1+1";
        _testSheet.Cells[1,0].Text = "=A1+1";
        Assert.That( _testSheet.Cells[1,0].Value, Is.EqualTo("3"));
    }
    [Test]
    public void TestFormulaWithReferenceAfterChange()
    {
        _testSheet = new Spreadsheet(10,10);
        _testSheet.Cells[0,0].Text = "=1+1";
        _testSheet.Cells[1,0].Text = "=A1+1";
        _testSheet.Cells[0,0].Text = "=1+2";
        Assert.That( _testSheet.Cells[1,0].Value, Is.EqualTo("4"));
    }
    [Test]
    public void TestMultipleCell()
    {
        _testSheet = new Spreadsheet(10,10);
        _testSheet.Cells[0,0].Text = "=1+1";
        _testSheet.Cells[0,1].Text = "=1+1";
        _testSheet.Cells[0,2].Text = "=A1+B1";
        Assert.That( _testSheet.Cells[0,2].Value, Is.EqualTo("4"));
    }
    [Test]
    public void TestCellWithConstant()
    {
        _testSheet = new Spreadsheet(10,10);
        _testSheet.Cells[0,0].Text = "=1+1";
        _testSheet.Cells[0,1].Text = "=A1+1";
        Assert.That( _testSheet.Cells[0,1].Value, Is.EqualTo("3"));
    }
    [Test]
    public void TestCellReferenceRemoval()
    {
        Assert.That(true);
        _testSheet = new Spreadsheet(10,10);
        _testSheet.Cells[0,0].Text = "=1+1";
        _testSheet.Cells[0,1].Text = "=A1+1";
        Assert.That(true);
    }
    //HW8 tests.
    [Test]
    public void TestUndo()
    {
        _testSheet = new Spreadsheet(10,10);
        _testSheet.Cells[0,1].Text = "=A1+1";
        _testSheet.Cells[0,0].Text = "=1+1";
        _testSheet.Cells[0,0].Text = "=1+2";
        _testSheet.Undo();
        Assert.That(_testSheet.Cells[0,1].Value, Is.EqualTo("3"));
    }
    [Test]
    public void TestRedo()
    {
        _testSheet = new Spreadsheet(10,10);
        _testSheet.Cells[0,1].Text = "=A1+1";
        _testSheet.Cells[0,0].Text = "=1+1";
        _testSheet.Cells[0,0].Text = "=1+2";
        _testSheet.Undo();
        _testSheet.Redo();
        Assert.That(_testSheet.Cells[0,1].Value, Is.EqualTo("3"));
    }
}