using System;
using UnityEngine;
using UnityEngine.Events;
using MSS.ScriptableEvents;
using System.Collections.Generic;

namespace MSS.ScriptableEvents
{
    public interface IEventData
    {
        public UnityAction Action { get; }
    }

    public interface IEventInvoker
    {
        void Invoke();
    }

    public interface IEventLogic
    {
        void AddListener(IEventListenerInvoker listener);
        void RemoveListener(IEventListenerInvoker listener);
    }

    public interface IEventData<T>
    {
        public UnityAction<T> Action { get; }
    }

    public interface IEventInvoker<T>
    {
        void Invoke(T data);
    }

    public interface IEventLogic<T>
    {
        void AddListener(IEventListenerInvoker<T> listener);
        void RemoveListener(IEventListenerInvoker<T> listener);
    }

    public interface IEventListenerData
    {
        UnityEvent Actions { get; }
        List<IEventLogic> EventsToListen { get; }
    }

    public interface IEventListenerInvoker
    {
        void OnInvoke();
        void Subscribe();
        void UnSubscribe();
    }

    public interface IEventListenerLogic
    {
        void AddEventToListen(IEventLogic baseEvent, bool updateSubscription = false);
        void RemoveEventToLister(IEventLogic baseEvent, bool updateSubscription = false);

    }

    public interface IEventListenerData<T>
    {
        UnityEvent<T> Actions { get; }
        List<IEventLogic<T>> EventsToListen { get; }
    }

    public interface IEventListenerInvoker<T>
    {
        void OnInvoke(T data);
        void Subscribe();
        void UnSubscribe();
    }

    public interface IEventListenerLogic<T>
    {
        void AddEventToListen(IEventLogic<T> baseEvent, bool updateSubscription = false);
        void RemoveEventToLister(IEventLogic<T> baseEvent, bool updateSubscription = false);

    }

    public interface IScriptableEventListenerData
    {
        UnityEvent Actions { get; }
        List<ScriptableBaseEvent> ScriptableEventsToListen { get; }
    }

    public interface IScriptableEventListenerLogic
    {
        void AddScriptableEventToListen(ScriptableBaseEvent scriptableBaseEvent, bool updateSubscription = false);
        void RemoveScriptableEventToLister(ScriptableBaseEvent scriptableBaseEvent, bool updateSubscription = false);
    }


    public interface IScriptableEventListenerData<T>
    {
        UnityEvent<T> Actions { get; }
        List<ScriptableBaseEvent<T>> ScriptableEventsToListen { get; }
    }

    public interface IScriptableEventListenerLogic<T>
    {
        void AddScriptableEventToListen(ScriptableBaseEvent<T> scriptableBaseEvent, bool updateSubscription = false);
        void RemoveScriptableEventToLister(ScriptableBaseEvent<T> scriptableBaseEvent, bool updateSubscription = false);
    }

    public interface IVariable<T>
    {
        T Value { get; set; }
        BaseEvent<T> OnValueChanged { get; set; }
    }
}

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

    public void AddListener(IEventListenerInvoker listener)
    {
        Debug.LogError($"Here Add Listener : {listener}");
        _onInvoke += listener.OnInvoke;
    }

    public void RemoveListener(IEventListenerInvoker listener)
    {
        Debug.LogError($"Here Remove Listener : {listener}");
        _onInvoke -= listener.OnInvoke;
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

    public void AddListener(IEventListenerInvoker<T> listener)
    {
        _onInvoke += listener.OnInvoke;
    }

    public void RemoveListener(IEventListenerInvoker<T> listener)
    {
        _onInvoke -= listener.OnInvoke;
    }

    #endregion
}

[System.Serializable]
public abstract class BaseEventListener : IEventListenerData, IEventListenerInvoker, IEventListenerLogic
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

    public void OnInvoke()
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
public abstract class BaseScriptableEventListener : IScriptableEventListenerData, IScriptableEventListenerLogic, IEventListenerInvoker
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

    public void OnInvoke()
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

public abstract class BaseEventListener<T> : IEventListenerData<T>, IEventListenerInvoker<T>, IEventListenerLogic<T>
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

    public void OnInvoke(T data)
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

public abstract class BaseScriptableEventListener<T> : BaseEventListener<T>, IScriptableEventListener<T>
{
    #region Members

    public override List<IEvent<T>> EventsToListen
    {
        get
        {
            if (_eventsToListen == null || _eventsToListen.Count != _scriptableEventsToListen.Count)
            {
                _eventsToListen = new();

                foreach (var scriptableEvent in _scriptableEventsToListen)
                    _eventsToListen.Add(scriptableEvent.Event);
            }

            return _eventsToListen;
        }
    }

    [SerializeField]
    protected List<ScriptableBaseEvent<T>> _scriptableEventsToListen = new();

    [property: SerializeField]
    public List<ScriptableBaseEvent<T>> ScriptableEventsToListen
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
        _eventsToListen.Add(scriptableBaseEvent.Event);

        if (updateSubscription)
            scriptableBaseEvent.Event.AddListener(this);
    }

    public void RemoveScriptableEventToLister(ScriptableBaseEvent<T> scriptableBaseEvent, bool updateSubscription = false)
    {
        _scriptableEventsToListen.Remove(scriptableBaseEvent);
        _eventsToListen.Remove(scriptableBaseEvent.Event);

        if (updateSubscription)
            scriptableBaseEvent.Event.RemoveListener(this);
    }


    public bool AreEventsSynched()
    {
        List<IEvent<T>> events = EventsToListen;

        if (events == null)
            return false;

        if (events.Count != _scriptableEventsToListen.Count)
            return false;

        for (int i = 0; i < events.Count; i++)
        {
            if (events[i] != _scriptableEventsToListen[i].Event)
                return false;
        }

        return true;
    }

    public void ForceSyncEvents()
    {
        _eventsToListen.Clear();

        foreach (var scriptableEvent in _scriptableEventsToListen)
        {
            AddEventToListen(scriptableEvent.Event);
        }
    }

    #endregion
}

[System.Serializable]
public class ScriptableIntEventListener : BaseScriptableEventListener<int>
{

}