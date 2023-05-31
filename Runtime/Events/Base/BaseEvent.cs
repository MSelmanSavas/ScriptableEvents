using System;
using UnityEngine;
using UnityEngine.Events;
using MSS.ScriptableEvents;
using System.Collections.Generic;

[System.Serializable]
public abstract class BaseEvent : IEventData, IEventInvoker, IEventLogic
{
    #region Members

    [SerializeField]
    protected event UnityAction _onInvoke;

    public UnityAction Action => _onInvoke;

    #endregion

    #region Public Methods

    public void Invoke()
    {
        _onInvoke?.Invoke();
    }

    public void AddListener(IEventInvoker listener)
    {
        _onInvoke += listener.Invoke;
    }

    public void RemoveListener(IEventInvoker listener)
    {
        _onInvoke -= listener.Invoke;
    }

    #endregion
}

[System.Serializable]
public abstract class BaseEvent<T> : IEventData<T>, IEventInvoker<T>, IEventLogic<T>
{
    #region Members

    [SerializeField]
    protected event UnityAction<T> _onInvoke;

    public UnityAction<T> Action => _onInvoke;

    #endregion

    #region Public Methods


    public void Invoke(T data)
    {
        _onInvoke?.Invoke(data);
    }

    public void AddListener(IEventInvoker<T> listener)
    {
        _onInvoke += listener.Invoke;
    }

    public void RemoveListener(IEventInvoker<T> listener)
    {
        _onInvoke -= listener.Invoke;
    }

    #endregion
}

[System.Serializable]
public abstract class BaseEventListener : IEventListenerData, IEventListenerSubsriber, IEventListenerLogic, IEventInvoker
{
    #region Members

    [SerializeField]
    protected UnityEvent _actions = new();

    public UnityEvent Actions { get => _actions; }

    [SerializeReference, SubclassSelector]
    protected List<IEventLogic> _eventsToListen = new();

    public virtual List<IEventLogic> EventsToListen => _eventsToListen;

    #endregion

    #region Public Methods

    public void Invoke()
    {
        _actions?.Invoke();
    }

    public void Subscribe()
    {
        foreach (var events in EventsToListen)
        {
            events.AddListener(this);
        }
    }

    public void UnSubscribe()
    {
        foreach (var events in EventsToListen)
        {
            events.RemoveListener(this);
        }
    }

    public void AddEventToListen(IEventLogic eventLogic, bool updateSubscription = false)
    {
        _eventsToListen.Add(eventLogic);

        if (updateSubscription)
            eventLogic.AddListener(this);
    }

    public void RemoveEventToLister(IEventLogic eventLogic, bool updateSubscription = false)
    {
        _eventsToListen.Remove(eventLogic);

        if (updateSubscription)
            eventLogic.RemoveListener(this);
    }

    #endregion
}

[System.Serializable]
public abstract class BaseScriptableEventListener : IScriptableEventListenerData, IScriptableEventListenerLogic, IEventListenerSubsriber, IEventInvoker
{
    #region Members

    [SerializeField]
    protected UnityEvent _actions = new();

    public UnityEvent Actions { get => _actions; }

    [SerializeField]
    protected List<ScriptableBaseEvent> _scriptableEventsToListen = new();

    [property: SerializeField]
    public virtual List<ScriptableBaseEvent> ScriptableEventsToListen
    {
        get => _scriptableEventsToListen;
        set
        {
            _scriptableEventsToListen = value;
        }
    }

    #endregion

    #region Public Methods

    public void AddScriptableEventToListen(ScriptableBaseEvent scriptableBaseEvent, bool updateSubscription = false)
    {
        _scriptableEventsToListen.Add(scriptableBaseEvent);

        if (updateSubscription)
            scriptableBaseEvent.EventLogic.AddListener(this);
    }

    public void RemoveScriptableEventToLister(ScriptableBaseEvent scriptableBaseEvent, bool updateSubscription = false)
    {
        _scriptableEventsToListen.Remove(scriptableBaseEvent);

        if (updateSubscription)
            scriptableBaseEvent.EventLogic.RemoveListener(this);
    }

    public void Invoke()
    {
        Actions?.Invoke();
    }

    public void Subscribe()
    {
        foreach (var scriptableEvent in _scriptableEventsToListen)
        {
            scriptableEvent.AddListener(this);
        }
    }

    public void UnSubscribe()
    {
        foreach (var scriptableEvent in _scriptableEventsToListen)
        {
            scriptableEvent.AddListener(this);
        }
    }

    #endregion
}

[System.Serializable]
public class VoidEventListener : BaseEventListener
{
}

[System.Serializable]
public class VoidEventListenerScriptable : BaseScriptableEventListener
{

}

public abstract class BaseEventListener<T> : IEventListenerData<T>, IEventListenerSubsriber, IEventListenerLogic<T>, IEventInvoker<T>
{
    #region Members

    [SerializeField]
    protected UnityEvent<T> _actions = new();

    public UnityEvent<T> Actions { get => _actions; }

    [SerializeReference, SubclassSelector]
    protected List<IEventLogic<T>> _eventsToListen = new();

    public virtual List<IEventLogic<T>> EventsToListen => _eventsToListen;

    #endregion

    #region Public Methods

    public void Invoke(T data)
    {
        Actions?.Invoke(data);
    }

    public void Subscribe()
    {
        foreach (var events in EventsToListen)
        {
            events.AddListener(this);
        }
    }

    public void UnSubscribe()
    {
        foreach (var events in EventsToListen)
        {
            events.RemoveListener(this);
        }
    }

    public void AddEventToListen(IEventLogic<T> eventLogic, bool updateSubscription = false)
    {
        _eventsToListen.Add(eventLogic);

        if (updateSubscription)
            eventLogic.AddListener(this);
    }

    public void RemoveEventToLister(IEventLogic<T> eventLogic, bool updateSubscription = false)
    {
        _eventsToListen.Remove(eventLogic);

        if (updateSubscription)
            eventLogic.RemoveListener(this);
    }

    #endregion
}

public abstract class BaseScriptableEventListener<T> : IScriptableEventListenerData<T>, IScriptableEventListenerLogic<T>, IEventListenerSubsriber, IEventInvoker<T>
{
    #region Members

    [SerializeField]
    protected UnityEvent<T> _actions = new();

    public UnityEvent<T> Actions { get => _actions; }

    [SerializeField]
    protected List<ScriptableBaseEvent<T>> _scriptableEventsToListen = new();

    [property: SerializeField]
    public virtual List<ScriptableBaseEvent<T>> ScriptableEventsToListen
    {
        get => _scriptableEventsToListen;
        set
        {
            _scriptableEventsToListen = value;
        }
    }

    #endregion

    #region Public Methods

    public void AddScriptableEventToListen(ScriptableBaseEvent<T> scriptableBaseEvent, bool updateSubscription = false)
    {
        _scriptableEventsToListen.Add(scriptableBaseEvent);

        if (updateSubscription)
            scriptableBaseEvent.EventLogic.AddListener(this);
    }

    public void RemoveScriptableEventToLister(ScriptableBaseEvent<T> scriptableBaseEvent, bool updateSubscription = false)
    {
        _scriptableEventsToListen.Remove(scriptableBaseEvent);

        if (updateSubscription)
            scriptableBaseEvent.EventLogic.RemoveListener(this);
    }

    public void Invoke(T data)
    {
        Actions?.Invoke(data);
    }

    public void Subscribe()
    {
        foreach (var scriptableEvent in _scriptableEventsToListen)
        {
            scriptableEvent.AddListener(this);
        }
    }

    public void UnSubscribe()
    {
        foreach (var scriptableEvent in _scriptableEventsToListen)
        {
            scriptableEvent.AddListener(this);
        }
    }

    #endregion
}

[System.Serializable]
public class ScriptableIntEventListener : BaseScriptableEventListener<int>
{

}