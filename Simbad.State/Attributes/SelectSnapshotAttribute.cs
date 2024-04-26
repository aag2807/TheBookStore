namespace Simbad.State.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SelectSnapshotAttribute : Attribute
{
    public Type StateType { get; }

    public SelectSnapshotAttribute(Type stateType)
    {
        StateType = stateType;
    }
}