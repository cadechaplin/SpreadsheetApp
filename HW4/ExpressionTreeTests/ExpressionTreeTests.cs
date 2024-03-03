// <copyright file="ExpressionTreeTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTreeTests;

using SpreadsheetEngine;

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
    [TestCase("5", ExpectedResult =5.0)]
    [TestCase("5+5+6*3-28", ExpectedResult =0.0)]
    [TestCase("5+5*6+3*2", ExpectedResult =41.0)]
    [TestCase("42-12-20", ExpectedResult =10.0)]
    public double Test(string exp)
    {
        ExpressionTree expTree = new ExpressionTree(exp);
        return expTree.Evaluate();
    }
}