using Simbad.State.Action;

namespace Simbad.State.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ActionAttribute : Attribute
{
    public string ActionName { get; }

    public ActionAttribute(Type actionType)
    {
        if (!typeof(IStateAction).IsAssignableFrom(actionType))
            throw new ArgumentException("Action type must implement IStateAction interface.");

        IStateAction? instance = Activator.CreateInstance(actionType) as IStateAction;
        ActionName = instance?.Type ?? throw new InvalidOperationException("Action type must provide a valid type.");
    }
}