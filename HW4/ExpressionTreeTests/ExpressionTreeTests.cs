// <copyright file="ExpressionTreeTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTreeTests;

using SpreadsheetEngine;
using System.Reflection;

/// <summary>
/// Tests for expression tree.
/// </summary>
public class ExpressionTreeTests
{
    /// <summary>
    /// Tests use of a variable.
    /// </summary>
    [Test]
    public void UsingAVariableTest()
    {
        ExpressionTree exp = new ExpressionTree("Hello+5");
        exp.SetVariable("Hello", 5.0);
        Assert.That(exp.Evaluate(), Is.EqualTo(10));
    }

    /// <summary>
    /// Various tests for each operator.
    /// </summary>
    /// <param name="exp">Expression that gets evaluated.</param>
    /// <returns> Returns Evaluation of expression Tree.</returns>
    [Test]
    [TestCase("3+5", ExpectedResult = 8.0)]
    [TestCase("100*10*10", ExpectedResult = 10000.0)]
    [TestCase("100+10+10", ExpectedResult = 120)]
    [TestCase("7-4-2", ExpectedResult = 1.0)]
    [TestCase("10-7-2", ExpectedResult = 1.0)]
    [TestCase("12-2-2", ExpectedResult = 8.0)]
    [TestCase("2+3-4+5", ExpectedResult = 6.0)]
    [TestCase("2+3-4+5", ExpectedResult = 6.0)]
    [TestCase("2+3+5", ExpectedResult = 10.0)]
    [TestCase("2*3*5", ExpectedResult = 30.0)]
    [TestCase("2*3*5", ExpectedResult = 30.0)]
    [TestCase("5/0", ExpectedResult = double.PositiveInfinity)]
    [TestCase("125/5/5", ExpectedResult = 5)]
    [TestCase("5", ExpectedResult = 5.0)]
    [TestCase("5+5+6*3-28", ExpectedResult = 0.0)]
    [TestCase("5+5*6+3*2", ExpectedResult = 41.0)]
    [TestCase("42-12-20", ExpectedResult = 10.0)]

    // Tests for Homework 5:
    [TestCase("42-12+20", ExpectedResult = 50.0)]
    [TestCase("2*4/2", ExpectedResult = 4.0)]
    [TestCase("2+4/2", ExpectedResult = 4.0)]
    [TestCase("4/2+5", ExpectedResult = 7.0)]
    [TestCase("5+6/3+2*4/2", ExpectedResult = 11.0)]
    [TestCase("(6+6)/(3+3)*4/2", ExpectedResult = 4.0)]
    [TestCase("(6*6)/(3*3)*4/2*(5*(2+4))", ExpectedResult = 240.0)]
    [TestCase("2^2", ExpectedResult = 4.0)]
    public double EtTests(string exp)
    {
        ExpressionTree expTree = new ExpressionTree(exp);
        return expTree.Evaluate();
    }

    [Test]
    public void ExpressionTree_MissingOperand()
    {
        Assert.That(() => new ExpressionTree("3+"), Throws.InvalidOperationException);
    }

    [Test]
    public void ShuntingTest_Addition()
    {
        ShuntingYard test = new ShuntingYard();
        Assert.That(test.ConvertToPostfix("3+5"), Is.EquivalentTo(new List<string> { "3", "5", "+" }));
    }

    [Test]
    public void ShuntingTest_AdditionAndDivision()
    {
        ShuntingYard test = new ShuntingYard();
        Assert.That(test.ConvertToPostfix("3+5/5"), Is.EquivalentTo(new List<string> { "3", "5", "5", "/", "+" }));
    }

    [Test]
    public void ShuntingTest_MultipleOperators()
    {
        ShuntingYard test = new ShuntingYard();
        Assert.That(test.ConvertToPostfix("3+5*4"), Is.EquivalentTo(new List<string> { "3", "5", "4", "*", "+" }));
    }

    [Test]
    public void ShuntingTest_MultipleCharLengthNums()
    {
        ShuntingYard test = new ShuntingYard();
        Assert.That(test.ConvertToPostfix("36+5*4"), Is.EquivalentTo(new List<string> { "36", "5", "4", "*", "+" }));
    }

    [Test]
    public void ShuntingTest_Parentheses()
    {
        ShuntingYard test = new ShuntingYard();
        Assert.That(test.ConvertToPostfix("(3+5)*4"), Is.EquivalentTo(new List<string> { "3", "5", "+", "4", "*" }));
    }

    [Test]
    public void ShuntingTest_Variable()
    {
        ShuntingYard test = new ShuntingYard();
        Assert.That(test.ConvertToPostfix("(3+a3b)*4"), Is.EquivalentTo(new List<string> { "3", "a3b", "+", "4", "*" }));
    }
    
    [Test]
    public void ShuntingTest_Variable2()
    {
        ShuntingYard test = new ShuntingYard();
        Assert.That(test.ConvertToPostfix("((3+a3b)*4)"), Is.EquivalentTo(new List<string> { "3", "a3b", "+", "4", "*" }));
    }
    
}