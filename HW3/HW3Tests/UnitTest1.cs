namespace HW3Tests;

public class Tests
{
    private FibonacciTextReader myFib;
    [SetUp]
    public void Setup()
    {
        myFib = new FibonacciTextReader(1);
    }

    [Test]
    public void FibTestNegativeVal()
    {
        myFib = new FibonacciTextReader(-1);
        Assert.AreEqual("",myFib.ReadToEnd());
    }
    [Test]
    public void FibTestsSpecialCase0()
    {
        myFib = new FibonacciTextReader(0);
        Assert.AreEqual("",myFib.ReadToEnd());
    }
    [Test]
    public void FibTestsSpecialCase1()
    {
        myFib = new FibonacciTextReader(1);
        Assert.AreEqual("1: 0\n",myFib.ReadToEnd());
    }
    [Test]
    public void FibTestGreaterThanSpecialCases()
    {
        myFib = new FibonacciTextReader(5);
        Assert.AreEqual("1: 0\n2: 1\n3: 1\n4: 2\n5: 3\n",myFib.ReadToEnd());
    }
    [Test]
    public void FibTestReadLine()
    {
        myFib = new FibonacciTextReader(5);
        Assert.AreEqual("1: 0",myFib.ReadLine());
        Assert.AreEqual("2: 1",myFib.ReadLine());
    }
}