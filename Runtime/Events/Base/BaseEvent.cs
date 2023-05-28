using System;
using UnityEngine;
using UnityEngine.Events;
using MSS.ScriptableEvents;
using System.Collections.Generic;

namespace MSS.ScriptableEvents
{
    public interface IEvent<T>
    {
        void Invoke(T data);
        void AddListener(IEventListener<T> listener);
        void RemoveListener(IEventListener<T> listener);
    }

    public interface IEvent
    {
        void Invoke();
        void AddListener(IEventListener listener);
        void RemoveListener(IEventListener listener);
    }

    public interface IEventListener
    {
        void OnInvoke();
        UnityEvent Actions { get; }
        List<IEvent> EventsToListen { get; }
        void AddEventToListen(IEvent baseEvent, bool updateSubscription = false);
        void RemoveEventToLister(IEvent baseEvent, bool updateSubscription = false);
        void Subscribe();
        void UnSubscribe();
    }

    public interface IScriptableEventListener : IEventListener
    {
        List<ScriptableBaseEvent> ScriptableEventsToListen { get; }
        bool AreEventsSynched();
        void ForceSyncEvents();
        void AddScriptableEventToListen(ScriptableBaseEvent scriptableBaseEvent, bool updateSubscription = false);
        void RemoveScriptableEventToLister(ScriptableBaseEvent scriptableBaseEvent, bool updateSubscription = false);
    }

    public interface IEventListener<T>
    {
        void OnInvoke(T data);
        UnityEvent<T> Actions { get; }
        List<IEvent<T>> EventsToListen { get; }
        void AddEventToListen(IEvent<T> baseEvent, bool updateSubscription = false);
        void RemoveEventToLister(IEvent<T> baseEvent, bool updateSubscription = false);
        void Subscribe();
        void UnSubscribe();
    }

    public interface IScriptableEventListener<T> : IEventListener<T>
    {
        List<ScriptableBaseEvent<T>> ScriptableEventsToListen { get; }
        bool AreEventsSynched();
        void ForceSyncEvents();
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
public abstract class BaseEvent : IEvent
{
    #region Members

    protected event Action _onInvoke;

    #endregion

    #region Public Methods

    public void Invoke()
    {
        _onInvoke?.Invoke();
    }

    public void AddListener(IEventListener listener)
    {
        _onInvoke += listener.OnInvoke;
    }

    public void RemoveListener(IEventListener listener)
    {
        _onInvoke -= listener.OnInvoke;
    }

    #endregion
}

[System.Serializable]
public abstract class BaseEvent<T> : IEvent<T>
{
    #region Members

    protected event Action<T> _onInvoke;

    #endregion

    #region Public Methods


    public void Invoke(T data)
    {
        _onInvoke?.Invoke(data);
    }

    public void AddListener(IEventListener<T> listener)
    {
        _onInvoke += listener.OnInvoke;
    }

    public void RemoveListener(IEventListener<T> listener)
    {
        _onInvoke -= listener.OnInvoke;
    }


    #endregion
}

[System.Serializable]
public abstract class BaseEventListener : IEventListener
{
    #region Members

    [SerializeField]
    protected UnityEvent _actions = new();

    public UnityEvent Actions { get => _actions; }

    [SerializeReference, SubclassSelector]
    protected List<IEvent> _eventsToListen = new();

    public virtual List<IEvent> EventsToListen => _eventsToListen;

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

    public void AddEventToListen(IEvent baseEvent, bool updateSubscription = false)
    {
        _eventsToListen.Add(baseEvent);

        if (updateSubscription)
            baseEvent.AddListener(this);
    }

    public void RemoveEventToLister(IEvent baseEvent, bool updateSubscription = false)
    {
        _eventsToListen.Remove(baseEvent);

        if (updateSubscription)
            baseEvent.RemoveListener(this);
    }


    #endregion
}

[System.Serializable]
public abstract class BaseScriptableEventListener : BaseEventListener, IScriptableEventListener
{
    #region Members

    public override List<IEvent> EventsToListen
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
        _eventsToListen.Add(scriptableBaseEvent.Event);

        if (updateSubscription)
            scriptableBaseEvent.Event.AddListener(this);
    }

    public void RemoveScriptableEventToLister(ScriptableBaseEvent scriptableBaseEvent, bool updateSubscription = false)
    {
        _scriptableEventsToListen.Remove(scriptableBaseEvent);
        _eventsToListen.Remove(scriptableBaseEvent.Event);

        if (updateSubscription)
            scriptableBaseEvent.Event.RemoveListener(this);
    }

    public bool AreEventsSynched()
    {
        List<IEvent> events = EventsToListen;

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
            _eventsToListen.Add(scriptableEvent.Event);
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

public abstract class BaseEventListener<T> : IEventListener<T>
{
    #region Members

    [SerializeField]
    protected UnityEvent<T> _listenerEvents = new UnityEvent<T>();

    public UnityEvent<T> Actions => _listenerEvents;

    [SerializeField]
    protected List<IEvent<T>> _eventsToListen = new();

    public virtual List<IEvent<T>> EventsToListen => _eventsToListen;

    #endregion

    #region Public Methods

    public void OnInvoke(T data)
    {
        _listenerEvents?.Invoke(data);
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

    public void AddEventToListen(IEvent<T> baseEvent, bool updateSubscription = false)
    {
        _eventsToListen.Add(baseEvent);

        if (updateSubscription)
            baseEvent.AddListener(this);
    }

    public void RemoveEventToLister(IEvent<T> baseEvent, bool updateSubscription = false)
    {
        _eventsToListen.Remove(baseEvent);

        if (updateSubscription)
            baseEvent.RemoveListener(this);
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