using Simbad.State.Action;

namespace Simbad.State.State;

public interface IStore
{
    /// <summary>
    /// Get's the current state of a state class
    /// </summary>
    /// <typeparam name="T">The StateClass</typeparam>
    public T GetState<T>() where T : class;

    /// <summary>
    /// Calls the "Reset" method of any given state class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void ResetState<T>() where T : class;

    /// <summary>
    /// Event that is called when the state changes 
    /// </summary>
    public event System.Action OnStateChanged;

    /// <summary>
    /// Dispatches an action implementing the IStateAction interface.
    /// </summary>
    /// <param name="action">The class implementing <see cref="IStateAction"/></param>
    /// <param name="payload">A payload for the action if any</param>
    void Dispatch(IStateAction action, params object[]? payload);
}