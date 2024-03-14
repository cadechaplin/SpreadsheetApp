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
    private readonly Dictionary<char, Type> nodeTypes;
    private readonly Dictionary<string, double> variableDictionary;

    // ReSharper restore InconsistentNaming

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
    /// </summary>
    /// <param name="expression">String of expression that will be used to construct expression tree.</param>
    public ExpressionTree(string expression)
    {
        this.variableDictionary = new Dictionary<string, double>();

        // Order here defines precedence.
        this.nodeTypes = new Dictionary<char, Type>
        {
            { '+', typeof(AdditionNode) },
            { '-', typeof(SubtractionNode) },
            { '*', typeof(MultiplicationNode) },
            { '/', typeof(DivisionNode) },

            // Add mappings for other operators as needed
        };
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
            return 0;
        }

        return this.root.Evaluate();
    }

    private ExpressionNode? Compile(string partition)
    {
        return string.IsNullOrEmpty(partition) ? null : this.CompileHelper(partition);
    }

    private ExpressionNode CompileHelper(string infix, string overload)
    {
        
        string ans = ShuntingYard.ConvertToPostfix(infix);
        ans = ans;
        
        return null;
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

        foreach (char operation in this.nodeTypes.Keys)
        {
            if (partition.Contains(operation))
            {
                var temp = OperatorNodeFactory.createNode(operation);
                int index = partition.LastIndexOf(operation);
                temp.Left = this.CompileHelper(partition.Substring(0, index));
                temp.Right = this.CompileHelper(partition.Substring(index + 1));
                return temp;
            }
        }

        if (this.variableDictionary.TryAdd(partition, 0))
        {
            // TODO: Not sure I should set to 0. Avoids errors.
        }

        return new VariableNode()
        {
            Name = partition,
            RefrenceDictionary = this.variableDictionary,
        };
    }
}