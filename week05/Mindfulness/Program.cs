using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

// Base Class for Activities
abstract class MindfulnessActivity
{
    protected int Duration;
    public void Start()
    {
        Console.Clear();
        Console.WriteLine($"Starting {GetType().Name}...");
        Console.WriteLine(GetDescription());
        Console.Write("Enter duration in seconds: ");
        Duration = int.Parse(Console.ReadLine() ?? "10");
        Console.WriteLine("Prepare to begin...");
        ShowAnimation(3);
        RunActivity();
        Console.WriteLine("Good job! You completed the activity.");
        ShowAnimation(3);
    }
    protected abstract string GetDescription();
    protected abstract void RunActivity();
    protected void ShowAnimation(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

// Breathing Activity
class BreathingActivity : MindfulnessActivity
{
    protected override string GetDescription() => "This activity helps you relax by guiding deep breathing.";
    protected override void RunActivity()
    {
        for (int i = 0; i < Duration / 6; i++)
        {
            Console.Write("Breathe in... ");
            ShowCountdown(3);
            Console.Write("Breathe out... ");
            ShowCountdown(3);
        }
    }
    private void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i + " ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

// Reflection Activity
class ReflectionActivity : MindfulnessActivity
{
    private static readonly List<string> Prompts = new()
    {
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };
    private static readonly List<string> Questions = new()
    {
        "Why was this experience meaningful to you?",
        "How did you feel when it was complete?",
        "What did you learn from it?"
    };
    protected override string GetDescription() => "This activity helps you reflect on times of strength and resilience.";
    protected override void RunActivity()
    {
        Console.WriteLine(Prompts[new Random().Next(Prompts.Count)]);
        ShowAnimation(3);
        for (int i = 0; i < Duration / 5; i++)
        {
            Console.WriteLine(Questions[new Random().Next(Questions.Count)]);
            ShowAnimation(5);
        }
    }
}

// Listing Activity
class ListingActivity : MindfulnessActivity
{
    private static readonly List<string> Prompts = new()
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are some of your personal heroes?"
    };
    protected override string GetDescription() => "This activity helps you list positive aspects of your life.";
    protected override void RunActivity()
    {
        Console.WriteLine(Prompts[new Random().Next(Prompts.Count)]);
        ShowAnimation(3);
        List<string> responses = new();
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        while (DateTime.Now < endTime)
        {
            Console.Write("Enter an item: ");
            responses.Add(Console.ReadLine() ?? "");
        }
        Console.WriteLine($"You listed {responses.Count} items!");
    }
}

// Main Program
class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine() ?? "";
            MindfulnessActivity? activity = choice switch
            {
                "1" => new BreathingActivity(),
                "2" => new ReflectionActivity(),
                "3" => new ListingActivity(),
                _ => null
            };
            if (activity == null) break;
            activity.Start();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
