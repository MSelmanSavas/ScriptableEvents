using MSS.ScriptableEvents;
using UnityEngine;

[System.Serializable]
public abstract class ScriptableBaseEvent : ScriptableObject
{
    public abstract IEvent Event { get; }

    [MethodButton]
    public void Invoke()
    {
        Event.Invoke();
    }

    public void AddListener(IEventListener listener)
    {
        Event.AddListener(listener);
    }

    public void RemoveListener(IEventListener listener)
    {
        Event.RemoveListener(listener);
    }
}

[System.Serializable]
public abstract class ScriptableBaseEvent<T> : ScriptableObject
{
    public abstract IEvent<T> Event { get; }

    public void Invoke(T data)
    {
        Event.Invoke(data);
    }

    public void AddListener(IEventListener<T> listener)
    {
        Event.AddListener(listener);
    }

    public void RemoveListener(IEventListener<T> listener)
    {
        Event.RemoveListener(listener);
    }
}


[System.Serializable]
public abstract class ScriptableBaseListener : ScriptableObject
{
    public abstract IScriptableEventListener Listener { get; }
}


[System.Serializable]
public abstract class ScriptableBaseListener<T> : ScriptableObject
{
    public abstract IScriptableEventListener<T> Listener { get; }
}

