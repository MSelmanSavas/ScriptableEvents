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

    public void AddListener(IEventListenerInvoker listener)
    {
        EventLogic.AddListener(listener);
    }

    public void RemoveListener(IEventListenerInvoker listener)
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

    public void AddListener(IEventListenerInvoker<T> listener)
    {
        EventLogic.AddListener(listener);
    }

    public void RemoveListener(IEventListenerInvoker<T> listener)
    {
        EventLogic.RemoveListener(listener);
    }
}


[System.Serializable]
public abstract class ScriptableBaseListener : ScriptableObject
{
    public bool ActiveInEditor = false;
    public abstract IEventListenerInvoker Listener { get; }

#if UNITY_EDITOR
    private void Awake()
    {
        Debug.LogError("Here Awake");

        if (Application.isEditor && !Application.isPlaying)
            if (ActiveInEditor)
            {
                Listener.UnSubscribe();
                Listener.Subscribe();
            }

    }

    private void OnEnable()
    {
        Debug.LogError("Here OnEnable");
        Debug.LogError($"Application.isEditor : {Application.isEditor}");
        Debug.LogError($"!Application.isPlaying : {!Application.isPlaying}");


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
    public abstract IEventListenerInvoker<T> Listener { get; }

#if UNITY_EDITOR
    private void Awake()
    {
        Debug.LogError("Here Awake");

        if (Application.isEditor && !Application.isPlaying)
            if (ActiveInEditor)
            {
                Listener.UnSubscribe();
                Listener.Subscribe();
            }

    }

    private void OnEnable()
    {
        Debug.LogError("Here OnEnable");
        Debug.LogError($"Application.isEditor : {Application.isEditor}");
        Debug.LogError($"!Application.isPlaying : {!Application.isPlaying}");


        if (Application.isEditor && !Application.isPlaying)
            if (ActiveInEditor)
            {
                Listener.UnSubscribe();
                Listener.Subscribe();
            }
    }

#endif
}

