using System;

public class EventController
{
    private Action _action;

    public void Invoke() => _action?.Invoke();
    
    public void AddListener(Action listener) => _action += listener;
    public void RemoveListener(Action listener) => _action -= listener;
}

public class EventController<T>
{
    private Action<T> _action;

    public void Invoke(T arg) => _action?.Invoke(arg);
    
    public void AddListener(Action<T> listener) => _action += listener;
    public void RemoveListener(Action<T> listener) => _action -= listener;
}

public class EventController<T1, T2>
{
    private Action<T1, T2> _action;

    public void Invoke(T1 arg1, T2 arg2) => _action?.Invoke(arg1, arg2);
    
    public void AddListener(Action<T1, T2> listener) => _action += listener;
    public void RemoveListener(Action<T1, T2> listener) => _action -= listener;
}