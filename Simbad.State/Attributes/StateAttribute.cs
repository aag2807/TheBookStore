
namespace Simbad.State.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class StateAttribute : Attribute
{
    public Type StateType { get; }
    public string Name { get; }

    public StateAttribute(Type stateType, string name)
    {
        StateType = stateType;
        Name = name;
    }
}