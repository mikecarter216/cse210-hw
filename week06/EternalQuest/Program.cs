using System;
using System.Collections.Generic;
using System.IO;

// Base Goal Class
abstract class Goal
{
    protected string Name;
    protected int Points;

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
    }

    public abstract int RecordEvent();
    public abstract string DisplayStatus();
    public virtual bool IsComplete() => false; 
}

// Simple Goal
class SimpleGoal : Goal
{
    private bool completed;

    public SimpleGoal(string name, int points) : base(name, points)
    {
        completed = false;
    }

    public override int RecordEvent()
    {
        if (!completed)
        {
            completed = true;
            return Points;
        }
        return 0;
    }

    public override string DisplayStatus()
    {
        return $"[{(completed ? "X" : " ")}] {Name} ({Points} pts)";
    }

    public override bool IsComplete() => completed;
}

// Eternal Goal
class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override int RecordEvent()
    {
        return Points;
    }

    public override string DisplayStatus()
    {
        return $"[∞] {Name} (+{Points} pts each time)";
    }
}

// Checklist Goal
class ChecklistGoal : Goal
{
    private int targetCount;
    private int currentCount;
    private int bonus;

    public ChecklistGoal(string name, int points, int targetCount, int bonus) : base(name, points)
    {
        this.targetCount = targetCount;
        this.bonus = bonus;
        currentCount = 0;
    }

    public override int RecordEvent()
    {
        if (currentCount < targetCount)
        {
            currentCount++;
            return (currentCount == targetCount) ? Points + bonus : Points;
        }
        return 0;
    }

    public override string DisplayStatus()
    {
        return $"[{currentCount}/{targetCount}] {Name} ({Points} pts, {bonus} bonus)";
    }

    public override bool IsComplete() => currentCount >= targetCount;
}

// Player Class
class Player
{
    public int Score { get; private set; }
    private List<Goal> goals;

    public Player()
    {
        Score = 0;
        goals = new List<Goal>();
    }

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void RecordGoal(int index)
    {
        if (index >= 0 && index < goals.Count)
        {
            Score += goals[index].RecordEvent();
        }
    }

    public void DisplayGoals()
    {
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].DisplayStatus()}");
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Total Score: {Score}");
    }

    // Save Data to File
    public void SaveData()
    {
        using (StreamWriter writer = new StreamWriter("player_data.txt"))
        {
            writer.WriteLine(Score);
        }
    }

    // Load Data from File
    public void LoadData()
    {
        if (File.Exists("player_data.txt"))
        {
            using (StreamReader reader = new StreamReader("player_data.txt"))
            {
                if (int.TryParse(reader.ReadLine(), out int savedScore))
                {
                    Score = savedScore;
                }
            }
        }
    }
}

// Main Program
class Program
{
    static void Main()
    {
        Player player = new Player();
        player.LoadData();

        while (true)
        {
            Console.WriteLine("\nEternal Quest Program");
            Console.WriteLine("1. Add Simple Goal");
            Console.WriteLine("2. Add Eternal Goal");
            Console.WriteLine("3. Add Checklist Goal");
            Console.WriteLine("4. Record Goal Event");
            Console.WriteLine("5. Show Goals");
            Console.WriteLine("6. Show Score");
            Console.WriteLine("7. Save & Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter goal name: ");
                    string simpleName = Console.ReadLine();
                    Console.Write("Enter points: ");
                    int simplePoints = int.Parse(Console.ReadLine());
                    player.AddGoal(new SimpleGoal(simpleName, simplePoints));
                    break;
                
                case "2":
                    Console.Write("Enter goal name: ");
                    string eternalName = Console.ReadLine();
                    Console.Write("Enter points per completion: ");
                    int eternalPoints = int.Parse(Console.ReadLine());
                    player.AddGoal(new EternalGoal(eternalName, eternalPoints));
                    break;

                case "3":
                    Console.Write("Enter goal name: ");
                    string checklistName = Console.ReadLine();
                    Console.Write("Enter points per completion: ");
                    int checklistPoints = int.Parse(Console.ReadLine());
                    Console.Write("Enter target count: ");
                    int target = int.Parse(Console.ReadLine());
                    Console.Write("Enter bonus points: ");
                    int bonus = int.Parse(Console.ReadLine());
                    player.AddGoal(new ChecklistGoal(checklistName, checklistPoints, target, bonus));
                    break;

                case "4":
                    player.DisplayGoals();
                    Console.Write("Select goal to record (number): ");
                    if (int.TryParse(Console.ReadLine(), out int index))
                    {
                        player.RecordGoal(index - 1);
                    }
                    break;

                case "5":
                    player.DisplayGoals();
                    break;

                case "6":
                    player.DisplayScore();
                    break;

                case "7":
                    player.SaveData();
                    Console.WriteLine("Game saved. Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}
