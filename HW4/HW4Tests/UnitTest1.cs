using SpreadsheetEngine;

namespace HW4Tests;

public class Tests
{
    private readonly Spreadsheet _testSheet = new Spreadsheet(10,10);
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void StandardCellTextChangeTest()
    {
        _testSheet.Cells[0,0].Text = "testing";
        Assert.That( _testSheet.Cells[0,0].Text, Is.EqualTo("testing"));
    }
    [Test]
    public void CellValueTest()
    {
        _testSheet.Cells[0,0].Text = "5";
        _testSheet.Cells[1,1].Text = "=A1";
        Assert.That( _testSheet.Cells[1,1].Value, Is.EqualTo("5"));
    }
    [Test]
    public void CellValueTestReference()
    {
        _testSheet.Cells[0,0].Text = "testing";
        Assert.That( _testSheet.Cells[0,0].Value, Is.EqualTo("testing"));
    }
    [Test]
    public void SpreadsheetCellOutOfRange()
    {
        Assert.Throws<IndexOutOfRangeException>(() => _testSheet.Cells[10, 10].Text = "testing");
        
    }
    [Test]
    public void SpreadsheetCellReferenceOutOfRange()
    {
        _testSheet.Cells[1, 1].Text = "=A12";
        Assert.That("!(bad reference)" == _testSheet.Cells[1, 1].Value);
        
    }
    [Test]
    public void SpreadsheetConstructorTest()
    {

        Assert.Throws<IndexOutOfRangeException>( () => new Spreadsheet(0, 0));
        

    }
}