using System.Diagnostics;
using Simbad.State.Attributes;
using Simbad.TestApp.Models;

namespace Simbad.TestApp.State;

[State(typeof(TodoListState), "Todo")]
public sealed class TodoListState
{
    public List<TodoItem> Items { get; } = new List<TodoItem>();

    [Action(typeof(AddTodo))]
    public void Add(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }

        Items.Add(new TodoItem(text));
    }

    [Action(typeof(RemoveTodo))]
    public void Remove(int index)
    {
        Items.RemoveAt(index);
    }

    [Action(typeof(ToggleTodoStatus))]
    public void ToggleStatus(int index)
    {
        TodoItem item = Items[index];
        if (item.IsComplete)
        {
            item.MarkIncomplete();
        }
        else
        {
            item.MarkComplete();
        }
    }
}