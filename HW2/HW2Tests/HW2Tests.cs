namespace HW2Tests;

public class Tests
{
    private HW2Prog goober;
    private List<int> testList;
    [SetUp]
    public void Setup()
    {
        goober = new HW2Prog();
        testList = [1, 1, 2, 2, 3, 4, 5, 7, 2, 5, 3, 2, 5, 2, 0];
    }

    [Test]
    public void GeneralMethod1Test()//test method 1
    {
        Assert.AreEqual( 7, goober.method1([1, 1, 2, 2, 3, 4, 5, 7, 2, 5, 3, 2, 5, 2, 0]));
        
    }

    [Test]
    public void Method1EmptyListTest()
    {
        Assert.AreEqual(0,goober.method1([]));
        
    }
    
    [Test]
    public void Method1DuplicatesTest()
    {
        Assert.AreEqual(1,goober.method1([0,0,0,0,0]));
        
    }
    [Test]
    public void Method1UniqueFirstElement()
    {
        Assert.AreEqual(2,goober.method1([1,0,0,0,0]));
    }
    [Test]
    public void Method1UniqueLastElement()
    {
        Assert.AreEqual(2,goober.method1([0,0,0,0,1]));
    }
    [Test]
    public void GeneralMethod2Test() //test method 2
    {
        Assert.AreEqual( 7, goober.method2([1, 1, 2, 2, 3, 4, 5, 7, 2, 5, 3, 2, 5, 2, 0]));
    }
    [Test]
    public void Method2EmptyListTest()
    {
        Assert.AreEqual( 0, goober.method2([]));
        
    }

    [Test]
    public void Method2DuplicatesTest()
    {
        Assert.AreEqual(1,goober.method2([0,0,0,0,0]));
        
    }
    [Test]
    public void Method2UniqueFirstElement()
    {
        Assert.AreEqual(2,goober.method2([1,0,0,0,0]));
    }
    [Test]
    public void Method2UniqueLastElement()
    {
        Assert.AreEqual(2,goober.method2([0,0,0,0,1]));
    }
    [Test]
    public void GeneralMethod3Test() //test method 3
    {
        Assert.AreEqual( 21, goober.method3([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21]));
    }
    [Test]
    public void Method3EmptyListTest()
    {
        Assert.AreEqual( 0, goober.method3([]));
        
    }

    [Test]
    public void Method3DuplicatesTest()
    {
        Assert.AreEqual(1,goober.method3([0,0,0,0,0]));
        
    }
    [Test]
    public void Method3UniqueFirstElement()
    {
        Assert.AreEqual(2,goober.method3([1,0,0,0,0]));
    }
    [Test]
    public void Method3UniqueLastElement()
    {
        Assert.AreEqual(2,goober.method3([0,0,0,0,1]));
    }
}