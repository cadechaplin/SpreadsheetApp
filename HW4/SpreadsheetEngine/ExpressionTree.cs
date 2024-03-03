using System.Runtime.CompilerServices;

namespace SpreadsheetEngine;

public class ExpressionTree
{
    private ExpressionNode root;
    private Dictionary<string, double> variableDictionary;
    
    private Dictionary<char, Type> nodeTypes;

    public ExpressionTree(string expression)
    {
        variableDictionary = new Dictionary<string, double>();
        nodeTypes = new Dictionary<char, Type>
        {
            { '+', typeof(AdditionNode) },
            { '-', typeof(SubtractionNode) },
            { '*', typeof(MultiplicationNode) },
            { '/', typeof(DivisionNode) },

            // Add mappings for other operators as needed
        };
        root = Compile(expression);
    }
    //method for setting a single variable.
    public void SetVariable(string variableName, double variableValue)
    {
        variableDictionary[variableName] = variableValue;
    }

    // Can use this method to set all variables if you already have the dictionary.
    public void SetVariable(Dictionary<string, double> preset)
    {
        variableDictionary = preset;
    }

    public double Evaluate()
    {
        return root.evaluate();
    }

    internal ExpressionNode Compile(string partition)
    {
        if (string.IsNullOrEmpty(partition))
        {
            return null;
        }
        // Remove spaces, since all they are is whitespace.
        partition = partition.Replace(" ", string.Empty);
        return CompileHelper(partition);
        //char[] operators = { '+', '-', '*', '/' };
        //throw new NotImplementedException();
    }

    internal ExpressionNode CompileHelper(string partition)
    {
        double number;
        // a constant
        if (double.TryParse(partition, out number))
        {
            // We need a ConstantNode
            return new ConstantNode()
            {
                Value = number
            };
        }

        foreach (char operation in nodeTypes.Keys)
        {
            if (partition.Contains(operation))
            {
                OperatorNode temp;
                //temp = new 
                Type cur = nodeTypes[operation];
                temp = (OperatorNode)Activator.CreateInstance(cur);
                int index = partition.IndexOf(operation);
                temp.Left = CompileHelper(partition.Substring(0, index));
                temp.Right = CompileHelper(partition.Substring(index + 1));
                return temp;
                // create instance of new cur.
            }
        }

        return null;
    }

    /*
    internal ExpressionNode CreateAdditionNode()
    {
        throw new NotImplementedException();
    }
    internal ExpressionNode CreateSubtractionNode()
    {
        throw new NotImplementedException();
    }
    internal ExpressionNode CreateDivisionNode()
    {
        throw new NotImplementedException();
    }
    internal ExpressionNode CreateDivisionNode()
    {
        throw new NotImplementedException();
    }
    */
    
}