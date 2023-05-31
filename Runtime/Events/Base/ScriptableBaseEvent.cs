using MSS.ScriptableEvents;
using UnityEngine;

[System.Serializable]
public abstract class ScriptableBaseEvent : ScriptableObject, IEventLogic, IEventInvoker
{
    public abstract IEventLogic EventLogic { get; }
    public abstract IEventInvoker EventInvoker { get; }

    [Sirenix.OdinInspector.Button]
    public void Invoke()
    {
        EventInvoker.Invoke();
    }

    public void AddListener(IEventInvoker listener)
    {
        EventLogic.AddListener(listener);
    }

    public void RemoveListener(IEventInvoker listener)
    {
        EventLogic.RemoveListener(listener);
    }
}

[System.Serializable]
public abstract class ScriptableBaseEvent<T> : ScriptableObject, IEventLogic<T>, IEventInvoker<T>
{
    public abstract IEventLogic<T> EventLogic { get; }
    public abstract IEventInvoker<T> EventInvoker { get; }

    [Sirenix.OdinInspector.Button]
    public void Invoke(T data)
    {
        EventInvoker.Invoke(data);
    }

    public void AddListener(IEventInvoker<T> listener)
    {
        EventLogic.AddListener(listener);
    }

    public void RemoveListener(IEventInvoker<T> listener)
    {
        EventLogic.RemoveListener(listener);
    }
}


[System.Serializable]
public abstract class ScriptableBaseListener : ScriptableObject
{
    public bool ActiveInEditor = false;
    public abstract IEventListenerSubsriber Listener { get; }

#if UNITY_EDITOR
    private void Awake()
    {
        if (Application.isEditor && !Application.isPlaying)
            if (ActiveInEditor)
            {
                Listener.UnSubscribe();
                Listener.Subscribe();
            }

    }

    private void OnEnable()
    {
        if (Application.isEditor && !Application.isPlaying)
            if (ActiveInEditor)
            {
                Listener.UnSubscribe();
                Listener.Subscribe();
            }
    }

#endif
}


[System.Serializable]
public abstract class ScriptableBaseListener<T> : ScriptableObject
{
    public bool ActiveInEditor = false;
    public abstract IEventListenerSubsriber Listener { get; }

#if UNITY_EDITOR
    private void Awake()
    {
        if (Application.isEditor && !Application.isPlaying)
            if (ActiveInEditor)
            {
                Listener.UnSubscribe();
                Listener.Subscribe();
            }

    }

    private void OnEnable()
    {
        if (Application.isEditor && !Application.isPlaying)
            if (ActiveInEditor)
            {
                Listener.UnSubscribe();
                Listener.Subscribe();
            }
    }

#endif
}

