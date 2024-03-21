// <copyright file="ShuntingYard.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine;

using System.Text;

/// <summary>
/// Shunting Yard class for evaluating expressions.
/// </summary>
public class ShuntingYard
{
    /// <summary>
    /// Factory instant for checking operator info.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    private readonly OperatorNodeFactory myFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShuntingYard"/> class.
    /// </summary>
    public ShuntingYard()
    {
        this.myFactory = new OperatorNodeFactory();
    }

    /// <summary>
    /// Factory instant for checking operator info.
    /// </summary>
    /// <returns> List of strings containing what should be used to create each node.</returns>
    /// <param name="infix"> String that will be used to create post fix notation.</param>
    public List<string> ConvertToPostfix(string infix)
    {
        List<string> postfix = new List<string>();
        Stack<char> operatorStack = new Stack<char>();
        StringBuilder currentToken = new StringBuilder();

        foreach (char token in infix)
        {
            if (token == '(')
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
            else if (this.IsOperator(token))
            {
                if (currentToken.Length > 0)
                {
                    postfix.Add(currentToken.ToString());
                    currentToken.Clear();
                }

                while (operatorStack.Count > 0 && this.ShouldPopOperator(operatorStack.Peek(), token))
                {
                    postfix.Add(operatorStack.Pop().ToString());
                }

                operatorStack.Push(token);
            }
            else
            {
                currentToken.Append(token);
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
    private bool IsOperator(char c)
    {
        if (this.myFactory.NodeTypes.ContainsKey(c))
        {
            return true;
        }

        return false;
    }

    // Determine whether to pop the top operator from the stack based on precedence and associativity
    private bool ShouldPopOperator(char op1, char op2)
    {
        // You can access precedence, associativity, and operator character from your node classes
        int precedenceOp1 = this.GetPrecedence(op1);
        int precedenceOp2 = this.GetPrecedence(op2);

        // If operators have the same precedence, pop left-associative operators
        if (precedenceOp1 == precedenceOp2)
        {
            return this.IsLeftAssociative(op1);
        }

        // Pop if precedence of op1 is greater than op2
        return precedenceOp1 > precedenceOp2;
    }

    // Get the precedence of an operator TODO Needs work
    private int GetPrecedence(char op)
    {
        // TODO probably the wrong place to handle this.
        if (op == '(' || op == ')')
        {
            return 0;
        }

        return this.myFactory.GetPrecedence(op);
    }

    // Determine if an operator is left-associative
    private bool IsLeftAssociative(char op)
    {
        if (this.myFactory.GetAssociativity(op) == Associativity.Left)
        {
            return true;
        }

        // Implement your logic to check associativity
        // You can use the Associativity property from your node classes
        // For demonstration purposes, let's assume all operators are left-associative
        return false;
    }
}