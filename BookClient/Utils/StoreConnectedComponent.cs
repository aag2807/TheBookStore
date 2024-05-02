using Microsoft.AspNetCore.Components;
using Simbad.State.State;

namespace BookClient.Utils;

public abstract class StoreConnectedComponent : ComponentBase, IDisposable
{
    [Inject] 
    protected IStore Store { get; set; }

    protected override void OnInitialized()
    {
        Store.OnStateChanged += HandleStateChange;
        base.OnInitialized();
    }

    private void HandleStateChange()
    {
        StateHasChanged();
        OnStateChange();
    }

    /// <summary>
    /// Abstract method that derived components can override to perform additional tasks when the state changes.
    /// </summary>
    protected virtual void OnStateChange()
    {
    }

    public void Dispose()
    {
        Store.OnStateChanged -= HandleStateChange;
    }
}