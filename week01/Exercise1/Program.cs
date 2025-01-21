using System;

class Program
{
    static void Main()
    {
        // Prompt for input and store it in a variable
        Console.Write("What is your favorite color? ");
        string color = Console.ReadLine();
        
        // Output the user's input with string interpolation
        Console.WriteLine($"Your favorite color is {color}.");
    }
}