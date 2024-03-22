namespace HW7Tests;
using SpreadsheetEngine;
public class Tests
{
    private readonly Spreadsheet _testSheet = new Spreadsheet(10,10);
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestFormula()
    {
        _testSheet.Cells[0,0].Text = "=1+1";
        Assert.That( _testSheet.Cells[0,0].Value, Is.EqualTo("2"));
    }
    [Test]
    public void TestFormulaWithReference()
    {
        _testSheet.Cells[0,0].Text = "=1+1";
        _testSheet.Cells[1,0].Text = "=A1+1";
        Assert.That( _testSheet.Cells[0,0].Value, Is.EqualTo("3"));
    }
    [Test]
    public void TestFormulaWithReferenceAfterChange()
    {
        _testSheet.Cells[0,0].Text = "=1+1";
        _testSheet.Cells[1,0].Text = "=A1+1";
        _testSheet.Cells[0,0].Text = "=1+2";
        Assert.That( _testSheet.Cells[0,0].Value, Is.EqualTo("4"));
    }
}