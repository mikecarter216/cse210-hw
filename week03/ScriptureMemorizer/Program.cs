using System;

class Program
{
    static void Main(string[] args)
    {
        // Create a scripture library and add a scripture manually (or load from file in advanced setup)
        var scriptureLibrary = new List<Scripture>
        {
            new Scripture(
                new Reference("Proverbs", "3:5", "3:6"),
                "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.")
        };

        // Choose a scripture (randomly or from index for now)
        Scripture scripture = scriptureLibrary[0];

        // Main program loop
        while (!scripture.IsFullyHidden())
        {
            Console.Clear();
            scripture.DisplayScripture();

            Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit.");
            string input = Console.ReadLine()?.Trim().ToLower();

            if (input == "quit")
            {
                Console.WriteLine("Goodbye!");
                return;
            }

            scripture.HideRandomWords(3); // Hide 3 random words each time
        }

        Console.Clear();
        scripture.DisplayScripture();
        Console.WriteLine("\nAll words are now hidden. Memorization complete!");
    }
}

class Reference
{
    public string Book { get; }
    public string StartVerse { get; }
    public string EndVerse { get; }

    public Reference(string book, string startVerse, string endVerse = null)
    {
        Book = book;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return EndVerse == null ? $"{Book} {StartVerse}" : $"{Book} {StartVerse}-{EndVerse}";
    }
}

class Word
{
    public string Text { get; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public string Display()
    {
        return IsHidden ? new string('_', Text.Length) : Text;
    }
}

class Scripture
{
    public Reference Reference { get; }
    private List<Word> Words { get; }

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void DisplayScripture()
    {
        Console.WriteLine(Reference);
        Console.WriteLine(string.Join(" ", Words.Select(word => word.Display())));
    }

    public void HideRandomWords(int count)
    {
        Random random = new Random();
        var wordsToHide = Words.Where(word => !word.IsHidden).OrderBy(_ => random.Next()).Take(count);

        foreach (var word in wordsToHide)
        {
            word.Hide();
        }
    }

    public bool IsFullyHidden()
    {
        return Words.All(word => word.IsHidden);
    }
}