using System;

class Program
{
    static void Main()
    {
        // Declare and initialize variables
        Console.Write("Enter your age: ");
        string userInput = Console.ReadLine();
        int age = int.Parse(userInput);  // Convert string input to integer

        // Conditional statements
        if (age >= 18)
        {
            Console.WriteLine("You are an adult.");
        }
        else if (age >= 13)
        {
            Console.WriteLine("You are a teenager.");
        }
        else
        {
            Console.WriteLine("You are a child.");
        }
    }
}
