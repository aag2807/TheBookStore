using System.Reflection;
using Simbad.State.Action;
using Simbad.State.Attributes;
using Simbad.State.State;

public sealed class Store : IStore
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<Type, object> _states = new Dictionary<Type, object>();
    private readonly Dictionary<string, (object Instance, MethodInfo Method)> _actionHandlers = new Dictionary<string, (object, MethodInfo)>();

    public Store(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        RegisterStates();
    }

    private void RegisterStates()
    {
        // Discover all state types decorated with the StateAttribute
        var stateTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttributes(typeof(StateAttribute), false).Any());

        foreach (var type in stateTypes)
        {
            // Create an instance of each state class
            var stateInstance = Activator.CreateInstance(type);
            _states[type] = stateInstance;

            // Register action handlers
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(m => m.GetCustomAttributes(typeof(ActionAttribute), false).Any());

            foreach (var method in methods)
            {
                var actionAttribute = method.GetCustomAttribute<ActionAttribute>();
                if (method.IsStatic || method.DeclaringType.IsInstanceOfType(stateInstance))
                {
                    _actionHandlers[actionAttribute.ActionName] = (stateInstance, method);
                }
            }
        }
    }

    public void Dispatch(StateAction action, object payload)
    {
        var actionName = action.Type;
        
        if (_actionHandlers.TryGetValue(actionName, out var handler))
        {
            handler.Method.Invoke(handler.Instance, new[] { payload });
        }
    }

    public T GetState<T>() where T : class
    {
        _states.TryGetValue(typeof(T), out var state);
        return state as T;
    }

    public void InjectStateSnapshots(object target)
    {
        var properties = target.GetType().GetProperties()
            .Where(prop => Attribute.IsDefined(prop, typeof(SelectSnapshotAttribute)));

        foreach (var property in properties)
        {
            var attribute = property.GetCustomAttribute<SelectSnapshotAttribute>();
            if (_states.TryGetValue(attribute.StateType, out var stateInstance) &&
                property.PropertyType.IsAssignableFrom(attribute.StateType))
            {
                property.SetValue(target, stateInstance);
            }
        }
    }
}