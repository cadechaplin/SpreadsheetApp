// <copyright file="OperatorNodeFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable SA1401
namespace SpreadsheetEngine;

using System.Reflection;

/// <summary>
/// Class for Creating and getting node info.
/// </summary>
public class OperatorNodeFactory
{
    /// <summary>
    /// Gets precedence using reflection.
    /// </summary>
    public readonly Dictionary<char, Type> NodeTypes =
        new Dictionary<char, Type>();

    /// <summary>
    /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
    /// </summary>
    public OperatorNodeFactory()
    {
        this.TraverseAvailableOperators((op, type) => this.NodeTypes.Add(op, type));
    }

    private delegate void OnOperator(char op, Type type);

    // ReSharper disable InconsistentNaming

    /// <summary>
    /// Gets precedence using reflection.
    /// </summary>
    /// <param name="op"> Operator for which precedence will be returned.</param>
    /// <returns> Precedence of operation passed to function.</returns>
    public int GetPrecedence(char op)
    {
        Type node = this.NodeTypes[op];
        FieldInfo? operatorField = node.GetField("Precedence");
        if (operatorField != null)
        {
            return (int)(operatorField.GetValue(node) ?? throw new InvalidOperationException());
        }

        throw new InvalidOperationException();
    }

    /// <summary>
    /// Gets associativity using reflection.
    /// </summary>
    /// <param name="op"> Operator for which associativity will be returned.</param>
    /// <returns> Associativity of operation passed to function.</returns>
    public Associativity GetAssociativity(char op)
    {
        Type node = this.NodeTypes[op];
        FieldInfo? operatorField = node.GetField("A");
        if (operatorField != null)
        {
            return (Associativity)(operatorField.GetValue(node) ?? throw new InvalidOperationException());
        }

        throw new InvalidOperationException();
    }

    /// <summary>
    /// Gets associativity using reflection.
    /// </summary>
    /// <param name="op"> Operator for which associativity will be returned.</param>
    /// <returns> Newly created node.</returns>
    internal OperatorNode CreateNode(char op)
    {
        if (this.NodeTypes.TryGetValue(op, out var type))
        {
            object? operatorNodeObject = Activator.CreateInstance(type);
            if (operatorNodeObject is OperatorNode)
            {
                return (OperatorNode)operatorNodeObject;
            }
        }

        throw new Exception("Unhandled operator");

        // Throw new NotImplementedException("Not implemented");
    }

    /// <summary>
    /// Finds all operator node types and adds them to the dictionary.
    /// </summary>
    private void TraverseAvailableOperators(OnOperator onOperator)
    {
        // This method will be implemented later
        Type operatorNodeType = typeof(OperatorNode);

        // Iterate over all loaded assemblies:
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            // Get all types that inherit from our OperatorNode class using LINQ
            IEnumerable<Type> operatorTypes =
                assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));
            foreach (var type in operatorTypes)
            {
                // for each subclass, retrieve the Operator property
                FieldInfo? operatorField = type.GetField("Operator");
                if (operatorField != null)
                {
                    // Get the character of the Operator
                    object? value = operatorField.GetValue(type);

                    // If “Operator” property is not static, you will need to create
                    // an instance first and use the following code instead (or similar):
                    // object value = operatorField.GetValue(Activator.CreateInstance(type,
                    // new ConstantNode(0)));
                    if (value is char)
                    {
                        char operatorSymbol = (char)value;

                        // And invoke the function passed as parameter
                        // with the operator symbol and the operator class
                        onOperator(operatorSymbol, type);
                    }
                }
            }
        }
    }
}