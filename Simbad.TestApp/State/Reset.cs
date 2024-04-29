using Simbad.State.Action;

namespace Simbad.TestApp.State;

public sealed class Reset : IStateAction
{
    public string Type { get; set; } = "Reset";
    
    public Reset()
    {
    }
}