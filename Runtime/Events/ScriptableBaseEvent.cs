using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewBaseEvent", menuName = "ScriptableEvents/Events/Create BaseEvent")]
public class ScriptableBaseEvent : ScriptableObject
{
    public BaseEvent Event;

    public void Invoke()
    {
        Event.Invoke();
    }

    public void AddListener(IListener listener)
    {
        Event.AddListener(listener);
    }

    public void RemoveListener(IListener listener)
    {
        Event.RemoveListener(listener);
    }
}

public abstract class ScriptableBaseEvent<T> : ScriptableObject
{
    public BaseEvent<T> Event;

     public void Invoke(T data)
    {
        Event.Invoke(data);
    }

    public void AddListener(IListener<T> listener)
    {
        Event.AddListener(listener);
    }

    public void RemoveListener(IListener<T> listener)
    {
        Event.RemoveListener(listener);
    }
}


[CreateAssetMenu(fileName = "NewBaseListener", menuName = "ScriptableEvents/Events/Create BaseListener")]
public class ScriptableBaseListener : ScriptableObject
{
    [SerializeReference]
    public BaseListener Listener;
}

public abstract class ScriptableBaseListener<T> : ScriptableObject
{
    [SerializeReference]
    public IListener<T> Listener;
}