using System.Reflection;
using Simbad.State.Action;
using Simbad.State.Attributes;
using Simbad.State.State;

public sealed class Store : IStore
{
    public event Action? OnStateChanged;

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
        List<Type> stateTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.GetCustomAttributes(typeof(Simbad.State.Attributes.StateAttribute), false).Any())
            .ToList();

        foreach (Type type in stateTypes)
        {
            object? stateInstance = Activator.CreateInstance(type);
            _states[type] = stateInstance;

            IEnumerable<MethodInfo> methods = type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(m => m.GetCustomAttributes(typeof(ActionAttribute), false).Any());

            foreach (MethodInfo method in methods)
            {
                ActionAttribute? actionAttribute = method.GetCustomAttribute<ActionAttribute>();
                if (method.IsStatic || method.DeclaringType.IsInstanceOfType(stateInstance))
                {
                    _actionHandlers[actionAttribute.ActionName] = (stateInstance, method);
                }
            }
        }
    }

    /// <inheritdoc />
    void IStore.Dispatch(IStateAction action, params object[]? payload)
    {
        string actionName = action.Type;

        if (_actionHandlers.TryGetValue(actionName, out var handler))
        {
            object[]? parameters = payload?.Length > 0 ? payload : null;
            handler.Method.Invoke(handler.Instance, parameters);

            OnStateChanged?.Invoke();
        }
    }


    /// <inheritdoc />
    T IStore.GetState<T>() where T : class
    {
        _states.TryGetValue(typeof(T), out var state);
        return state as T;
    }

    /// <inheritdoc />
    void IStore.ResetState<T>() where T : class
    {
        if (_states.TryGetValue(typeof(T), out var state))
        {
            MethodInfo? resetMethod = state.GetType().GetMethod("Reset");
            resetMethod?.Invoke(state, null);
            OnStateChanged?.Invoke();
        }
    }
}