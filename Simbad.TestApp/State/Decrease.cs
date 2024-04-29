using Simbad.State.Action;

namespace Simbad.TestApp.State;

public sealed class Decrease : IStateAction
{
    public string Type { get; set; } = "Decrease";
}