using Simbad.State.Action;

namespace BookClient.Services.State.Layout.actions;

public sealed class OpenModal : IStateAction
{
    public string Type { get; set; } = "[Layout] Open Modal";
}