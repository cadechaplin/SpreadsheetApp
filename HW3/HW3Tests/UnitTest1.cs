namespace HW3Tests;

public class Tests
{
    private FibonacciTextReader? myFib;
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void FibTestNegativeVal()
    {
        myFib = new FibonacciTextReader(-1);
        Assert.That(myFib.ReadToEnd(),Is.EqualTo(""));
        
    }
    [Test]
    public void FibTestsSpecialCase0()
    {
        myFib = new FibonacciTextReader(0);
        Assert.That(myFib.ReadToEnd(),Is.EqualTo(""));
    }
    [Test]
    public void FibTestsSpecialCase1()
    {
        myFib = new FibonacciTextReader(1);
        Assert.That(myFib.ReadToEnd(),Is.EqualTo("1: 0\n"));
    }
    [Test]
    public void FibTestGreaterThanSpecialCases()
    {
        myFib = new FibonacciTextReader(5);
        Assert.That(myFib.ReadToEnd(),Is.EqualTo("1: 0\n2: 1\n3: 1\n4: 2\n5: 3\n"));
    }
    [Test]
    public void FibTestReadLine()
    {
        myFib = new FibonacciTextReader(5);
        Assert.That(myFib.ReadLine(),Is.EqualTo("1: 0"));
        Assert.That(myFib.ReadLine(),Is.EqualTo("2: 1"));
    }
    [Test]
    public void FibTestBigNumber()
    {
        myFib = new FibonacciTextReader(100);
        for (int i = 0; i < 99; i++)
        {
            myFib.ReadLine();
        }
        Assert.That(myFib.ReadLine(),Is.EqualTo("100: 218922995834555169026"));
    }
}