// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine;

/// <summary>
/// ExpressionTree Class creates expression trees from a string and can evaluate them.
/// </summary>
public class ExpressionTree
{
    // ReSharper disable InconsistentNaming
    private readonly ExpressionNode? root;
    private readonly Dictionary<char, Type> NodeTypes;
    private readonly Dictionary<string, double> variableDictionary;
    private OperatorNodeFactory myfactory = new OperatorNodeFactory();

    // ReSharper restore InconsistentNaming

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
    /// </summary>
    /// <param name="expression">String of expression that will be used to construct expression tree.</param>
    public ExpressionTree(string expression)
    {
        this.variableDictionary = new Dictionary<string, double>();
        this.root = this.Compile(expression);
    }

    // method for setting a single variable.

    /// <summary>
    /// Set a variable in the variable dictionary.
    /// </summary>
    /// <param name="variableName">Key for dictionary entry.</param>
    /// <param name="variableValue">Value for dictionary entry.</param>
    public void SetVariable(string variableName, double variableValue)
    {
        this.variableDictionary[variableName] = variableValue;
    }

    /// <summary>
    /// Initiates evaluation of the tree.
    /// </summary>
    /// <returns> Returns the evaluation of the tree.</returns>
    public double Evaluate()
    {
        if (this.root == null)
        {
            return 0.0;
        }

        return this.root.Evaluate();
    }

    private ExpressionNode? Compile(string partition)
    {
        ShuntingYard alg = new ShuntingYard();
        return string.IsNullOrEmpty(partition) ? null : this.CompileHelper(alg.ConvertToPostfix(partition));
    }

    private ExpressionNode CompileHelper(List<string> postfix)
    {
        Stack<ExpressionNode> pStack = new Stack<ExpressionNode>();
        foreach (string arg in postfix)
        {
            ExpressionNode temp = nodify(arg);

            if (temp is OperatorNode op)
            {
                op.Right = pStack.Pop();
                op.Left = pStack.Pop();
            }
            pStack.Push(temp);
        }

        return pStack.Pop();
    }

    private ExpressionNode nodify(string arg)
    {
        if (double.TryParse(arg, out double result))
        {
            // Parsing successful, create a ConstantNode with the parsed integer
            ConstantNode constantNode = new ConstantNode(){
                Value = result,
            };

            // Now you can use the constantNode object
            // For example: return constantNode;

            // If you want to return the constantNode directly, you can do:
            return constantNode;
        }

        if (arg.Length is 1 && myfactory.NodeTypes.ContainsKey(arg[0]))
        {
            return myfactory.CreateNode(arg[0]);
        }

        return new VariableNode()
        {
            Name = arg,
            ReferenceDictionary = this.variableDictionary,
        };

    }

    private ExpressionNode CompileHelper(string partition)
    {
        if (double.TryParse(partition, out var number))
        {
            // We need a ConstantNode
            return new ConstantNode()
            {
                Value = number,
            };
        }

        foreach (char operation in this.NodeTypes.Keys)
        {
            if (partition.Contains(operation))
            {
                var temp = myfactory.CreateNode(operation);
                int index = partition.LastIndexOf(operation);
                temp.Left = this.CompileHelper(partition.Substring(0, index));
                temp.Right = this.CompileHelper(partition.Substring(index + 1));
                return temp;
            }
        }

        this.variableDictionary.TryAdd(partition, 0);
        return new VariableNode()
        {
            Name = partition,
            ReferenceDictionary = this.variableDictionary,
        };
    }
}