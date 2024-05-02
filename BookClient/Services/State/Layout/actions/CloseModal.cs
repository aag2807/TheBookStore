using Simbad.State.Action;

namespace BookClient.Services.State.Layout.actions;

public sealed class CloseModal : IStateAction
{
    public string Type { get; set; } = "[Layout] Toggle Modal Visibility";
}