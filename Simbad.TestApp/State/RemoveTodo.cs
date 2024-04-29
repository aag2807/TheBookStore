using Simbad.State.Action;

namespace Simbad.TestApp.State;

public class RemoveTodo: IStateAction
{
    public string Type { get; set; } = "[Todo] Remove";
}