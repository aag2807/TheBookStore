using Simbad.State.Action;

namespace Simbad.TestApp.State;

public sealed class AddTodo : IStateAction
{
    public string Type { get; set; } = "[Todo] Add";
}