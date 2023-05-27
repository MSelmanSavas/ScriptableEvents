using System;
using UnityEngine;
using UnityEngine.Events;

public interface IEvent<T>
{
    void Invoke(T data);
    void AddListener(IListener<T> listener);
    void RemoveListener(IListener<T> listener);
}

public interface IEvent
{
    void Invoke();
    void AddListener(IListener listener);
    void RemoveListener(IListener listener);
}

public interface IListener
{
    void OnInvoke();
    UnityEvent Listener { get; }
}

public interface IListener<T>
{
    void OnInvoke(T data);
    UnityEvent<T> Listener { get; }
}

public interface IVariable<T>
{
    T Value { get; set; }
    BaseEvent<T> OnValueChanged { get; set; }
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

    public void AddListener(IListener listener)
    {
        _onInvoke += listener.OnInvoke;
    }

    public void RemoveListener(IListener listener)
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

    public void AddListener(IListener<T> listener)
    {
        _onInvoke += listener.OnInvoke;
    }

    public void RemoveListener(IListener<T> listener)
    {
        _onInvoke -= listener.OnInvoke;
    }


    #endregion
}

public abstract class BaseListener : IListener
{
    #region Members

    [SerializeField]
    protected UnityEvent _listenerEvents = new UnityEvent();

    public UnityEvent Listener { get => _listenerEvents; }

    #endregion

    #region Public Methods

    public void OnInvoke()
    {
        _listenerEvents?.Invoke();
    }

    #endregion
}

public abstract class BaseListener<T> : IListener<T>
{
    #region Members

    [SerializeField]
    protected UnityEvent<T> _listenerEvens = new UnityEvent<T>();

    public UnityEvent<T> Listener => _listenerEvens;

    #endregion

    #region Public Methods

    public void OnInvoke(T data)
    {
        _listenerEvens?.Invoke(data);
    }

    #endregion
}
