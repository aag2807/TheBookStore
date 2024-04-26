namespace Simbad.State.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ActionAttribute : Attribute
{
    public string ActionName { get; }

    public ActionAttribute(string actionName)
    {
        ActionName = actionName;
    }
}