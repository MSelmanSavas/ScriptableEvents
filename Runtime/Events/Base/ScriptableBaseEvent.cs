using MSS.ScriptableEvents;
using UnityEngine;

[System.Serializable]
public abstract class ScriptableBaseEvent : ScriptableObject
{
    public BaseEvent Event;

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
    public BaseEvent<T> Event;

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
    [SerializeReference]
    public IEventListener Listener;
}


[System.Serializable]
public abstract class ScriptableBaseListener<T> : ScriptableObject
{
    [SerializeReference]
    public IEventListener<T> Listener;
}

