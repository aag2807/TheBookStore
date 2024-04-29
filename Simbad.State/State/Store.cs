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
        var stateTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.GetCustomAttributes(typeof(Simbad.State.Attributes.StateAttribute), false).Any())
            .ToList();

        foreach (var type in stateTypes)
        {
            var stateInstance = Activator.CreateInstance(type);
            _states[type] = stateInstance;

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

    public void Dispatch(IStateAction action, object? payload = null)
    {
        string actionName = action.Type;
        
        if (_actionHandlers.TryGetValue(actionName, out var handler))
        {
            if (payload is null)
            {
                handler.Method.Invoke(handler.Instance, null);                
            }
            else
            {
                handler.Method.Invoke(handler.Instance, new[] { payload });
            }
        }
    }

    public T GetState<T>() where T : class
    {
        _states.TryGetValue(typeof(T), out var state);
        return state as T;
    }
}