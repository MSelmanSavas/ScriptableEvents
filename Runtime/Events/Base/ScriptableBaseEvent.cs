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
    public bool ActiveInEditor = false;
    public abstract IScriptableEventListener Listener { get; }

#if UNITY_EDITOR
    private void Awake()
    {
        Debug.LogError("Here Awake");

        if (Application.isEditor && !Application.isPlaying)
            if (ActiveInEditor)
            {
                if (!Listener.AreEventsSynched())
                {
                    Listener.ForceSyncEvents();
                }

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
                if (!Listener.AreEventsSynched())
                {
                    Listener.ForceSyncEvents();
                }

                Listener.UnSubscribe();
                Listener.Subscribe();
            }
    }

    private void OnValidate()
    {
        Debug.LogError("Here OnValidate");
        Debug.LogError($"Application.isEditor : {Application.isEditor}");
        Debug.LogError($"!Application.isPlaying : {!Application.isPlaying}");

        if (Application.isEditor && !Application.isPlaying)
            if (ActiveInEditor)
            {
                if (!Listener.AreEventsSynched())
                {
                    Listener.ForceSyncEvents();
                }
            }
    }

#endif
}


[System.Serializable]
public abstract class ScriptableBaseListener<T> : ScriptableObject
{
    public abstract IScriptableEventListener<T> Listener { get; }
}

