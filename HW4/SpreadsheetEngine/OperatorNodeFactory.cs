namespace SpreadsheetEngine;

public static class OperatorNodeFactory
{
    // ReSharper disable InconsistentNaming
    public static Dictionary<char, Type> nodeTypes =
        new Dictionary<char, Type>
        {
            { '+', typeof(AdditionNode) },
            { '-', typeof(SubtractionNode) },
            { '*', typeof(MultiplicationNode) },
            { '/', typeof(DivisionNode) },

            // Add mappings for other operators as needed
        };

    internal static OperatorNode createNode(char op)
    {
        return (OperatorNode)Activator.CreateInstance(nodeTypes[op])!;

        // Throw new NotImplementedException("Not implemented");
    }
}