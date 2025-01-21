using System;

class Program
{
    static void Main()
    {
        string playAgain = "yes"; // Loop to play again
        Random randomGenerator = new Random(); // Random number generator for future random number games
        
        while (playAgain == "yes")
        {
            // Step 1: Ask for the magic number and guess
            Console.Write("What is the magic number? ");
            int magicNumber = int.Parse(Console.ReadLine()); // User input for the magic number

            int guess = 0;
            int guessCount = 0;

            // Step 2: Guessing loop
            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine()); // User input for the guess
                guessCount++;

                // Step 3: Provide feedback on whether the guess is too high or low
                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            }

            // Step 4: Display the number of guesses made
            Console.WriteLine($"It took you {guessCount} guesses.");

            // Step 5: Ask if the user wants to play again
            Console.Write("Do you want to play again? (yes/no): ");
            playAgain = Console.ReadLine();
        }
    }
}
