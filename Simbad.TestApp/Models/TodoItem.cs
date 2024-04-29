namespace Simbad.TestApp.Models;

public sealed class TodoItem
{
    public string Text { get; private set; }
    public bool IsComplete { get; private set; }
    public DateTime CompletedAt { get; private set; }

    public TodoItem(string text)
    {
        Text = text;
    }
    
    public void MarkComplete()
    {
        IsComplete = true;
        CompletedAt = DateTime.Now;
    }
    
    public void MarkIncomplete()
    {
        IsComplete = false;
        CompletedAt = DateTime.MinValue;
    }
}