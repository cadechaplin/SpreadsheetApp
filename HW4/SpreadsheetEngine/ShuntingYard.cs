using System;
using System.Collections.Generic;
using System.Text;
using SpreadsheetEngine;
public class ShuntingYard
{
    public static List<string> ConvertToPostfix(string infix)
    {
        List<string> postfix = new List<string>();
        Stack<char> operatorStack = new Stack<char>();

        StringBuilder currentToken = new StringBuilder();

        foreach (char token in infix)
        {
            if (char.IsDigit(token))
            {
                currentToken.Append(token);
            }
            else if (token == '(')
            {
                if (currentToken.Length > 0)
                {
                    postfix.Add(currentToken.ToString());
                    currentToken.Clear();
                }
                operatorStack.Push(token);
            }
            else if (token == ')')
            {
                if (currentToken.Length > 0)
                {
                    postfix.Add(currentToken.ToString());
                    currentToken.Clear();
                }
                while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                {
                    postfix.Add(operatorStack.Pop().ToString());
                }
                operatorStack.Pop(); // Discard the '('
            }
            else if (IsOperator(token))
            {
                if (currentToken.Length > 0)
                {
                    postfix.Add(currentToken.ToString());
                    currentToken.Clear();
                }
                while (operatorStack.Count > 0 && ShouldPopOperator(operatorStack.Peek(), token))
                {
                    postfix.Add(operatorStack.Pop().ToString());
                }
                operatorStack.Push(token);
            }
        }

        if (currentToken.Length > 0)
        {
            postfix.Add(currentToken.ToString());
        }

        // Pop remaining operators from the stack
        while (operatorStack.Count > 0)
        {
            postfix.Add(operatorStack.Pop().ToString());
        }

        return postfix;
    }

    // Check if a character is an operator
    private static bool IsOperator(char c)
    {
        if (OperatorNodeFactory.nodeTypes.ContainsKey(c))
        {
            return true;
        }

        return false;
    }

    // Determine whether to pop the top operator from the stack based on precedence and associativity
    private static bool ShouldPopOperator(char op1, char op2)
    {
        // You can access precedence, associativity, and operator character from your node classes
        int precedenceOp1 = GetPrecedence(op1);
        int precedenceOp2 = GetPrecedence(op2);

        // If operators have the same precedence, pop left-associative operators
        if (precedenceOp1 == precedenceOp2)
        {
            return IsLeftAssociative(op1);
        }

        // Pop if precedence of op1 is greater than op2
        return precedenceOp1 > precedenceOp2;
    }

    // Get the precedence of an operator
    private static int GetPrecedence(char op)
    {
        switch (op)
        {
            case '+':
            case '-':
                return 1;
            case '*':
            case '/':
                return 2;
            default:
                throw new ArgumentException("Invalid operator: " + op);
        }
    }

    // Determine if an operator is left-associative
    private static bool IsLeftAssociative(char op)
    {
        // Implement your logic to check associativity
        // You can use the Associativity property from your node classes
        // For demonstration purposes, let's assume all operators are left-associative
        return true;
    }
}