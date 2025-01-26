using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        MindfulnessProgram program = new MindfulnessProgram();
        program.Start();
    }
}

public class MindfulnessProgram
{
    public void Start()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Mindfulness Program!");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Select an activity: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                BreathingActivity breathingActivity = new BreathingActivity();
                breathingActivity.StartActivity();
                breathingActivity.ActivitySpecificLogic();
            }
            else if (choice == "2")
            {
                ReflectionActivity reflectionActivity = new ReflectionActivity();
                reflectionActivity.StartActivity();
                reflectionActivity.ActivitySpecificLogic();
            }
            else if (choice == "3")
            {
                ListingActivity listingActivity = new ListingActivity();
                listingActivity.StartActivity();
                listingActivity.ActivitySpecificLogic();
            }
            else if (choice == "4")
            {
                Console.WriteLine("Thank you for using the Mindfulness Program. Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
}

// Base class for all activities
public abstract class MindfulnessActivity
{
    protected int duration;

    public void StartActivity()
    {
        ShowMessage("Welcome to the activity!");
        GetDuration();
        ShowMessage("Prepare to begin...");
        Pause(3);  // Pause for 3 seconds before starting
    }

    // Abstract method to be implemented by derived classes
    public abstract void ActivitySpecificLogic();

    protected void ShowMessage(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
        Console.WriteLine();
    }

    protected void GetDuration()
    {
        Console.Write("Enter the duration of the activity in seconds: ");
        duration = int.Parse(Console.ReadLine());
    }

    protected void Pause(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);  // Pause for 1 second between dots
        }
        Console.WriteLine();
    }
}

// Breathing Activity
public class BreathingActivity : MindfulnessActivity
{
    public override void ActivitySpecificLogic()
    {
        ShowMessage("This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");

        for (int i = 0; i < duration; i += 4)
        {
            ShowMessage("Breathe in...");
            Pause(3);  // Breathe in for 3 seconds
            ShowMessage("Breathe out...");
            Pause(3);  // Breathe out for 3 seconds
        }

        ShowMessage("Good job! You've completed the breathing activity.");
    }
}

// Reflection Activity
public class ReflectionActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "How did you feel when it was complete?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public override void ActivitySpecificLogic()
    {
        Random random = new Random();
        int promptIndex = random.Next(prompts.Count);
        ShowMessage(prompts[promptIndex]);
        Pause(3);  // Pause for 3 seconds to think about the prompt

        ShowMessage("Now, reflect on the following questions:");
        for (int i = 0; i < duration; i++)
        {
            int questionIndex = random.Next(questions.Count);
            ShowMessage(questions[questionIndex]);
            Pause(5);  // Pause for 5 seconds for reflection
        }

        ShowMessage("Good job! You've completed the reflection activity.");
    }
}

// Listing Activity
public class ListingActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?"
    };

    public override void ActivitySpecificLogic()
    {
        Random random = new Random();
        int promptIndex = random.Next(prompts.Count);
        ShowMessage(prompts[promptIndex]);
        Pause(3);  // Pause for 3 seconds to think about the prompt

        Console.WriteLine("Start listing now! Type 'done' when finished.");
        List<string> listItems = new List<string>();

        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            string input = Console.ReadLine();
            if (input.ToLower() == "done")
            {
                break;
            }
            else
            {
                listItems.Add(input);
            }
        }

        ShowMessage($"You listed {listItems.Count} items.");
        ShowMessage("Good job! You've completed the listing activity.");
    }
}
