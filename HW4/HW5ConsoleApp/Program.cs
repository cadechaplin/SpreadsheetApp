// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using SpreadsheetEngine;

var quit = false;
var expStr = string.Empty;
var varDict = new Dictionary<string, double>();
while (!quit)
{
    Console.WriteLine($"Menu: (Current Expression: {expStr} )");
    Console.WriteLine("1. Enter new expression");
    Console.WriteLine("2. Set variable");
    Console.WriteLine("3. Evaluate");
    Console.WriteLine("4. Quit");
    Console.WriteLine("Enter your choice (1-4) or type 'quit' to exit: ");
    string? choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            // Add code for Option 1 here
            Console.WriteLine("Enter new expression:");
            expStr = Console.ReadLine();
            break;
        case "2": // TODO: not sure if I should have a dictionary of my variables here.
            Console.WriteLine("Enter new Variable name:");
            var curKey = Console.ReadLine();
            Console.WriteLine("Enter variable value:");
            var curVal = Console.ReadLine();
            if (double.TryParse(curVal, out var value))
            {
                if (curKey != null)
                {
                    varDict[curKey] = value;
                    Console.WriteLine($"Variable '{curKey}' set to {value}");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for variable value. Please enter an integer.");
            }

            break;
        case "3":
            if (expStr != null)
            {
                ExpressionTree temp = new ExpressionTree(expStr);
                foreach (var key in varDict.Keys)
                {
                    temp.SetVariable(key, varDict[key]);
                }

                Console.WriteLine(temp.Evaluate());
            }

            break;
        case "4":
            quit = true;
            break;
        default:
            Console.WriteLine("Invalid choice. Please enter a number between 1 and 4, or type 'quit' to exit.");
            break;
    }

    Console.WriteLine();
}