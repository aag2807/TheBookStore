using Simbad.State.Action;

namespace Simbad.TestApp.State;

public sealed class ToggleTodoStatus : IStateAction
{
    public string Type { get; set; } = "[Todo] Toggle todo status";
}