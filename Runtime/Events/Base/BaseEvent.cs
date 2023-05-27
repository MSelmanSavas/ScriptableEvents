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
        List<BaseEvent> EventsToListen { get; }
        void AddEventToListen(BaseEvent baseEvent, bool updateSubscription = false);
        void RemoveEventToLister(BaseEvent baseEvent, bool updateSubscription = false);
        void Subscribe();
        void UnSubscribe();
    }

    public interface IScriptableEventListener : IEventListener
    {
        List<ScriptableBaseEvent> ScriptableEventsToListen { get; }
        void AddScriptableEventToListen(ScriptableBaseEvent scriptableBaseEvent, bool updateSubscription = false);
        void RemoveScriptableEventToLister(ScriptableBaseEvent scriptableBaseEvent, bool updateSubscription = false);
    }

    public interface IEventListener<T>
    {
        void OnInvoke(T data);
        UnityEvent<T> Actions { get; }
        List<BaseEvent<T>> EventsToListen { get; }
        void AddEventToListen(BaseEvent<T> baseEvent, bool updateSubscription = false);
        void RemoveEventToLister(BaseEvent<T> baseEvent, bool updateSubscription = false);
        void Subscribe();
        void UnSubscribe();
    }

    public interface IScriptableEventListener<T> : IEventListener<T>
    {
        List<ScriptableBaseEvent<T>> ScriptableEventsToListen { get; }
        void AddScriptableEventToListen(ScriptableBaseEvent<T> scriptableBaseEvent, bool updateSubscription = false);
        void RemoveScriptableEventToLister(ScriptableBaseEvent<T> scriptableBaseEvent, bool updateSubscription = false);
    }

    public interface IVariable<T>
    {
        T Value { get; set; }
        BaseEvent<T> OnValueChanged { get; set; }
    }
}



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

    [SerializeField]
    protected List<BaseEvent> _eventsToListen = new();

    public virtual List<BaseEvent> EventsToListen => _eventsToListen;

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

    public void AddEventToListen(BaseEvent baseEvent, bool updateSubscription = false)
    {
        _eventsToListen.Add(baseEvent);

        if (updateSubscription)
            baseEvent.AddListener(this);
    }

    public void RemoveEventToLister(BaseEvent baseEvent, bool updateSubscription = false)
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

    public override List<BaseEvent> EventsToListen
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

    #endregion
}

[System.Serializable]
public class ScriptableVoidEventListener : BaseScriptableEventListener
{

}

public class VoidEventListener : BaseEventListener
{

}

public abstract class BaseEventListener<T> : IEventListener<T>
{
    #region Members

    [SerializeField]
    protected UnityEvent<T> _listenerEvents = new UnityEvent<T>();

    public UnityEvent<T> Actions => _listenerEvents;

    [SerializeField]
    protected List<BaseEvent<T>> _eventsToListen = new();

    public virtual List<BaseEvent<T>> EventsToListen => _eventsToListen;

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

    public void AddEventToListen(BaseEvent<T> baseEvent, bool updateSubscription = false)
    {
        _eventsToListen.Add(baseEvent);

        if (updateSubscription)
            baseEvent.AddListener(this);
    }

    public void RemoveEventToLister(BaseEvent<T> baseEvent, bool updateSubscription = false)
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

    public override List<BaseEvent<T>> EventsToListen
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

    #endregion
}

[System.Serializable]
public class ScriptableIntEventListener : BaseScriptableEventListener<int>
{

}