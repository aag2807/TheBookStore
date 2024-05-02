using BookClient.Services.State.Layout.actions;
using Simbad.State.Attributes;

namespace BookClient.Services.State.Layout;

[State(typeof(LayoutState), "Layout")]
public sealed class LayoutState
{
    public bool IsModalOpen { get; private set; } = false;
    public Type? ModalComponentType { get; set; } = null;
    public Dictionary<string, object> ModalComponentArgs { get; private set; } = new();

    [Action(typeof(CloseModal))]
    public void CloseModal()
    {
        IsModalOpen = false;
        ModalComponentType = null;
        ModalComponentArgs.Clear();
    }

    [Action(typeof(OpenModal))]
    public void OpenModal(Type modalComponentType, Dictionary<string, object> args)
    {
        ModalComponentType = modalComponentType;
        ModalComponentArgs = args;
        IsModalOpen = true;
    }
}