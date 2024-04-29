namespace Simbad.State.interfaces;

public interface IResetOnLeave
{
    /// <summary>
    /// The Reset method is called when the state is left. 
    /// </summary>
    public void Reset();
}