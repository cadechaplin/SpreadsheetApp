// See https://aka.ms/new-console-template for more information
using SpreadsheetEngine;

bool quit = false;
string expStr = string.Empty;
Dictionary<string, int> varDict = new Dictionary<string, int>();
string curKey = String.Empty;
string curVal = String.Empty;
while (!quit)
{
    Console.WriteLine($"Menu: (Current Expression: {expStr} )");
    Console.WriteLine("1. Enter new expression");
    Console.WriteLine("2. Set variable");
    Console.WriteLine("3. Evaluate");
    Console.WriteLine("4. Quit");
    Console.WriteLine("Enter your choice (1-4) or type 'quit' to exit: ");
    
    string choice = Console.ReadLine();
    
    switch (choice)
    {
        case "1":
            // Add code for Option 1 here
            Console.WriteLine("Enter new expression:");
            expStr = Console.ReadLine();
            break;
        case "2": // not sure if I should have a dictionary of my variables here.
            // Add code for Option 2 here
            Console.WriteLine("Enter new Variable name:");
            curKey = Console.ReadLine();
            Console.WriteLine("Enter variable value:");
            curVal = Console.ReadLine();
            int value;
            if (int.TryParse(curVal, out value))
            {
                varDict[curKey] = value;
                Console.WriteLine($"Variable '{curKey}' set to {value}");
            }
            else
            {
                Console.WriteLine("Invalid input for variable value. Please enter an integer.");
            }
            break;
        case "3":
            // Add code for Option 3 here
            ExpressionTree temp = new ExpressionTree(expStr);
            foreach (var key in varDict.Keys)
            {
                temp.SetVariable(key, varDict[key]);
            }
            Console.WriteLine(temp.Evaluate());
            break;
        case "4":
            Console.WriteLine("You chose Option 4");
            // Add code for Option 4 here
            quit = true;
            break;
        default:
            Console.WriteLine("Invalid choice. Please enter a number between 1 and 4, or type 'quit' to exit.");
            break;
    }
    
    Console.WriteLine();
}