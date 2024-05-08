namespace Core.Boundaries.Persistance.Util;

public sealed class Criteria
{
    /// <summary>
    /// Represents the property name
    /// </summary>
    public string PropertyName { get; set; } = string.Empty;

    /// <summary>
    /// Represents the operation
    /// </summary>
    public Operation Operation { get; set; }

    /// <summary>
    /// Represents the value of filter
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Creates an instance of <see cref="Criteria"/>
    /// </summary>
    /// <param name="propertyName">Represents the property name</param>
    /// <param name="operation">Represents the operation to compare property name with value</param>
    /// <param name="value">Represents the value to compare</param>
    public Criteria(string propertyName, Operation operation, string value)
    {
        PropertyName = propertyName;
        Operation = operation;
        Value = value;
    }
}