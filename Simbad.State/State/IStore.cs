using Simbad.State.Action;

namespace Simbad.State.State;

public interface IStore
{
    public void Dispatch(StateAction action, object payload);
}