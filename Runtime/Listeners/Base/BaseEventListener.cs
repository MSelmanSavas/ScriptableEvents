using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MSS.ScriptableEvents.Events;

namespace MSS.ScriptableEvents.Listeners
{
    [System.Serializable]
    public abstract class BaseEventListener : IEventListenerData, IEventListenerSubscriber, IEventListenerLogic, IEventListenerInvoker
    {
        #region Members

        [SerializeField]
        protected UnityEvent _onInvokedActions = new();

        public virtual UnityEvent OnInvokedActions { get => _onInvokedActions; }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
#endif
        protected List<IEventLogic> _eventsToListen = new();

        public virtual List<IEventLogic> EventsToListen => _eventsToListen;

        #endregion

        #region Public Methods

        public virtual void OnInvoked()
        {
            _onInvokedActions?.Invoke();
        }

        public virtual void Subscribe()
        {
            foreach (var events in EventsToListen)
            {
                events.AddListener(this);
            }
        }

        public virtual void UnSubscribe()
        {
            foreach (var events in EventsToListen)
            {
                events.RemoveListener(this);
            }
        }

        public virtual void AddEventToListen(IEventLogic eventLogic, bool updateSubscription = false)
        {
            _eventsToListen.Add(eventLogic);

            if (updateSubscription)
                eventLogic.AddListener(this);
        }

        public virtual void RemoveEventToListen(IEventLogic eventLogic, bool updateSubscription = false)
        {
            _eventsToListen.Remove(eventLogic);

            if (updateSubscription)
                eventLogic.RemoveListener(this);
        }

        #endregion
    }

    [System.Serializable]
    public abstract class BaseEventListener<T> : IEventListenerData<T>, IEventListenerSubscriber, IEventListenerLogic<T>, IEventListenerInvoker<T>
    {
        #region Members

        [SerializeField]
        protected UnityEvent<T> _oInvokedActions = new();

        public virtual UnityEvent<T> OnInvokedActions { get => _oInvokedActions; }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
#endif
        protected List<IEventLogic<T>> _eventsToListen = new();

        public virtual List<IEventLogic<T>> EventsToListen => _eventsToListen;

        #endregion

        #region Public Methods

        public virtual void OnInvoked(T data)
        {
            OnInvokedActions?.Invoke(data);
        }

        public virtual void Subscribe()
        {
            foreach (var events in EventsToListen)
            {
                events.AddListener(this);
            }
        }

        public virtual void UnSubscribe()
        {
            foreach (var events in EventsToListen)
            {
                events.RemoveListener(this);
            }
        }

        public virtual void AddEventToListen(IEventLogic<T> eventLogic, bool updateSubscription = false)
        {
            _eventsToListen.Add(eventLogic);

            if (updateSubscription)
                eventLogic.AddListener(this);
        }

        public virtual void RemoveEventToListen(IEventLogic<T> eventLogic, bool updateSubscription = false)
        {
            _eventsToListen.Remove(eventLogic);

            if (updateSubscription)
                eventLogic.RemoveListener(this);
        }

        #endregion
    }

}