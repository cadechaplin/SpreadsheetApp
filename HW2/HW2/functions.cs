using System;
using System.Collections.Generic;

class Program
{
    public void run()
    {
        // Creating a List of integers
        List<int> numbers = new List<int>();

        // Adding elements to the list
        numbers.Add(1);
        numbers.Add(2);
        numbers.Add(3);

        // Accessing elements
        Console.WriteLine("Elements in the list:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }

        // Removing an element
        numbers.Remove(2);

        // Accessing elements after removal
        Console.WriteLine("Elements in the list after removal:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
    }
}