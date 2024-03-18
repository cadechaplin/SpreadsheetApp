using System.Reflection;
namespace SpreadsheetEngine;

public class OperatorNodeFactory
{
    private delegate void OnOperator(char op, Type type);

    public OperatorNodeFactory()
    {
        
        TraverseAvailableOperators((op, type) => nodeTypes.Add(op, type));
        
    }
    
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
                FieldInfo operatorField = type.GetField("Operator");
                if (operatorField != null)
                {
// Get the character of the Operator
                    object value = operatorField.GetValue(type);
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
    
    // ReSharper disable InconsistentNaming
    public Dictionary<char, Type> nodeTypes =
        new Dictionary<char, Type>();

    internal OperatorNode createNode(char op)
    {
        if (nodeTypes.ContainsKey(op))
        {
            object operatorNodeObject = System.Activator.CreateInstance(nodeTypes[op]);
            if (operatorNodeObject is OperatorNode)
            {
                return (OperatorNode)operatorNodeObject;
            }
        }
        throw new Exception("Unhandled operator");

        // Throw new NotImplementedException("Not implemented");
    }
}