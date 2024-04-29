using Simbad.State.Action;

namespace Simbad.TestApp.State;

public sealed class Increment : IStateAction
{
    public string Type { get; set; } = "Increment";
    
    public Increment()
    {
    }
}