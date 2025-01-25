using System;
using System.Collections.Generic;

class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of comments: {GetCommentCount()}");

        foreach (var comment in Comments)
        {
            Console.WriteLine($"Comment by {comment.Name}: {comment.Text}");
        }

        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        // Create videos
        Video video1 = new Video("How to Code in C#", "Michael Akpan", 180);
        Video video2 = new Video("Learning Python for Beginners", "Michael Akpan", 150);
        Video video3 = new Video("Understanding JavaScript", "Michael Akpan", 200);

        // Add comments to video 1
        video1.AddComment(new Comment("John", "Great tutorial!"));
        video1.AddComment(new Comment("Emily", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Sarah", "Can't wait to try coding myself!"));

        // Add comments to video 2
        video2.AddComment(new Comment("David", "I learned a lot from this."));
        video2.AddComment(new Comment("Anna", "Simple and easy to understand."));

        // Add comments to video 3
        video3.AddComment(new Comment("Chris", "I love how you explain the concepts."));
        video3.AddComment(new Comment("Jordan", "I wish this was available when I started!"));

        // Create a list of videos
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Iterate through the list and display video details
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}