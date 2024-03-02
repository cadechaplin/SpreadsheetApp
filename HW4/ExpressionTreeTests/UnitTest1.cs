namespace ExpressionTreeAppTests;
using SpreadsheetEngine;
public class Tests
{
   
    [SetUp]
    public void Setup()
    {
        
        
    }

    [Test]
    public void GeneralExpressionTest()
    {
        ExpressionTree exp = new ExpressionTree("3*3*3");
        Assert.That(27, Is.EqualTo(exp.Evaluate()));
    }

    [Test]
    [TestCase("3+5", ExpectedResult = 8.0)]
    [TestCase("100/10*10", ExpectedResult = 100.0)]
    [TestCase("100/(10*10)", ExpectedResult = 1.0)]
    [TestCase("7-4+2", ExpectedResult = 5.0)]
    [TestCase("10/(7-2)", ExpectedResult = 2.0)]
    [TestCase("(12-2)/2", ExpectedResult = 5.0)]
    [TestCase("((((((2+3)-(4+5))))))", ExpectedResult = -4.0)]
    [TestCase("2*3+5", ExpectedResult = 11.0)]
    [TestCase("2+3*5", ExpectedResult = 17.0)]
    [TestCase("2 * 3 * 5", ExpectedResult = 17.0)]
    [TestCase("5/0", ExpectedResult = double.PositiveInfinity)]
    public double Test(string exp)
    {
        ExpressionTree expTree = new ExpressionTree(exp);
        return expTree.Evaluate();
    }
}