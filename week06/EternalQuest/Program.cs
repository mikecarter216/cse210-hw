using System;
using System.Collections.Generic;
using System.IO;

// Base class
abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }
    public bool IsComplete { get; protected set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        IsComplete = false;
    }

    public abstract void RecordEvent();
    public abstract string GetStatus();
    public abstract string SaveGoal();
}

// Simple goal class
class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) {}

    public override void RecordEvent()
    {
        IsComplete = true;
    }

    public override string GetStatus()
    {
        return IsComplete ? "[X] " + Name : "[ ] " + Name;
    }

    public override string SaveGoal()
    {
        return $"Simple,{Name},{Points},{IsComplete}";
    }
}

// Eternal goal class
class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) {}

    public override void RecordEvent()
    {
        Points += Points;
    }

    public override string GetStatus()
    {
        return "[∞] " + Name;
    }

    public override string SaveGoal()
    {
        return $"Eternal,{Name},{Points}";
    }
}

// Checklist goal class
class ChecklistGoal : Goal
{
    public int TargetCount { get; set; }
    public int CurrentCount { get; set; }
    public int BonusPoints { get; set; }

    public ChecklistGoal(string name, int points, int targetCount, int bonusPoints) 
        : base(name, points)
    {
        TargetCount = targetCount;
        BonusPoints = bonusPoints;
        CurrentCount = 0;
    }

    public override void RecordEvent()
    {
        CurrentCount++;
        if (CurrentCount >= TargetCount)
        {
            IsComplete = true;
        }
    }

    public override string GetStatus()
    {
        return IsComplete ? $"[X] {Name} Completed!" : $"[ ] {Name} ({CurrentCount}/{TargetCount})";
    }

    public override string SaveGoal()
    {
        return $"Checklist,{Name},{Points},{CurrentCount},{TargetCount},{BonusPoints}";
    }
}

// Goal Manager (handles saving and loading goals)
class GoalManager
{
    public List<Goal> Goals { get; private set; } = new List<Goal>();
    public int TotalScore { get; private set; } = 0;

    public void AddGoal(Goal goal)
    {
        Goals.Add(goal);
    }

    public void RecordGoal(string name)
    {
        foreach (Goal goal in Goals)
        {
            if (goal.Name == name)
            {
                goal.RecordEvent();
                TotalScore += goal.Points;
                Console.WriteLine($"Event recorded! You earned {goal.Points} points.");
                return;
            }
        }
        Console.WriteLine("Goal not found!");
    }

    public void ShowGoals()
    {
        foreach (Goal goal in Goals)
        {
            Console.WriteLine(goal.GetStatus());
        }
        Console.WriteLine($"Total Score: {TotalScore}");
    }

    public void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            writer.WriteLine(TotalScore);
            foreach (Goal goal in Goals)
            {
                writer.WriteLine(goal.SaveGoal());
            }
        }
    }

    public void LoadGoals()
    {
        if (!File.Exists("goals.txt")) return;
        using (StreamReader reader = new StreamReader("goals.txt"))
        {
            TotalScore = int.Parse(reader.ReadLine());
            Goals.Clear();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts[0] == "Simple") Goals.Add(new SimpleGoal(parts[1], int.Parse(parts[2])));
                else if (parts[0] == "Eternal") Goals.Add(new EternalGoal(parts[1], int.Parse(parts[2])));
                else if (parts[0] == "Checklist") Goals.Add(new ChecklistGoal(parts[1], int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4])));
            }
        }
    }
}

// Main Program
class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();
        manager.LoadGoals();

        while (true)
        {
            Console.WriteLine("\nEternal Quest - Goal Tracker");
            Console.WriteLine("1. Add Goal");
            Console.WriteLine("2. Record Goal");
            Console.WriteLine("3. Show Goals");
            Console.WriteLine("4. Save and Exit");
            Console.Write("Select an option: ");
            
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Write("Enter goal name: ");
                string name = Console.ReadLine();
                Console.Write("Enter points: ");
                int points = int.Parse(Console.ReadLine());
                Console.Write("Type (1=Simple, 2=Eternal, 3=Checklist): ");
                int type = int.Parse(Console.ReadLine());
                
                if (type == 1) manager.AddGoal(new SimpleGoal(name, points));
                else if (type == 2) manager.AddGoal(new EternalGoal(name, points));
                else if (type == 3)
                {
                    Console.Write("Enter target count: ");
                    int target = int.Parse(Console.ReadLine());
                    Console.Write("Enter bonus points: ");
                    int bonus = int.Parse(Console.ReadLine());
                    manager.AddGoal(new ChecklistGoal(name, points, target, bonus));
                }
            }
            else if (choice == "2")
            {
                Console.Write("Enter goal name to record event: ");
                manager.RecordGoal(Console.ReadLine());
            }
            else if (choice == "3")
            {
                manager.ShowGoals();
            }
            else if (choice == "4")
            {
                manager.SaveGoals();
                Console.WriteLine("Goals saved. Exiting...");
                break;
            }
        }
    }
}
