using Simbad.State.Attributes;

namespace Simbad.TestApp.State;

[StateAttribute(typeof(CounterState), "CounterState")]
public sealed class CounterState
{
    public int Value { get; private set; } = 0;

    [Action("Increment")]
    public void Increment(int amount)
    {
        Value += amount;
        Console.WriteLine($"Counter incremented by {amount}, new value: {Value}.");
    }

    [Action("Reset")]
    public void Reset()
    {
        Value = 0;
        Console.WriteLine("Counter reset to 0.");
    }
    
    [Action("Decrease")]
    public void Decrease(int amount)
    {
        Value -= amount;
        Console.WriteLine($"Counter decreased by {amount}, new value: {Value}.");
    }
}