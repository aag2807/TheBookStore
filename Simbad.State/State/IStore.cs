using Simbad.State.Action;

namespace Simbad.State.State;

public interface IStore
{
    public void Dispatch(IStateAction action, object? payload = null);

    public T GetState<T>() where T : class;
}