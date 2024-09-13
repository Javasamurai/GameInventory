using System;

public class EventController
{
    private Action _action;

    public void Invoke() => _action?.Invoke();
    
    public void AddListener(Action listener) => _action += listener;
    public void RemoveListener(Action listener) => _action -= listener;
}