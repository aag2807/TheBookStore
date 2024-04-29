using Triplex.Validations;

namespace Core.Utils;

using System;
using System.Reflection;

public static class ObjectUtils
{
    public static void Assign(object target, object source)
    {
        Arguments.NotNull(target, nameof(target));
        Arguments.NotNull(source, nameof(source));

        Type targetType = target.GetType();
        Type sourceType = source.GetType();

        PropertyInfo[] sourceProperties = sourceType.GetProperties();
        foreach (var sourceProperty in sourceProperties)
        {
            PropertyInfo? targetProperty = targetType.GetProperty(sourceProperty.Name);
            if (targetProperty != null && targetProperty.CanWrite)
            {
                object? value = sourceProperty.GetValue(source);
                targetProperty.SetValue(target, value);
            }
        }
    }
}