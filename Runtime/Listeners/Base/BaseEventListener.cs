using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MSS.ScriptableEvents.Events;

namespace MSS.ScriptableEvents.Listeners
{
    [System.Serializable]
    public class BaseEventListener : IEventListenerData, IEventListenerSubscriber, IEventListenerLogic, IEventListenerInvoker
    {
        #region Members

        [SerializeField]
        protected UnityEvent onInvokedActions = new();
        public virtual UnityEvent OnInvokedActions { get => onInvokedActions; }

        protected virtual List<IReadOnlyCollection<IEventLogic>> addonEventsCollections => new();

        protected List<IEventLogic> eventsToListen = new();
        public virtual List<IEventLogic> EventsToListen => eventsToListen;

        #endregion

        #region Public Methods


#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public virtual bool Initialize()
        {
            try
            {
                foreach (IReadOnlyCollection<IEventLogic> collection in addonEventsCollections)
                {
                    foreach (IEventLogic eventLogic in collection)
                    {
                        eventsToListen.Add(eventLogic);
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return false;
            }

            return true;
        }

        public virtual void OnInvoked()
        {
            onInvokedActions?.Invoke();
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
            eventsToListen.Add(eventLogic);

            if (updateSubscription)
                eventLogic.AddListener(this);
        }

        public virtual void RemoveEventToListen(IEventLogic eventLogic, bool updateSubscription = false)
        {
            eventsToListen.Remove(eventLogic);

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
        protected UnityEvent<T> onInvokedActions = new();

        public virtual UnityEvent<T> OnInvokedActions { get => onInvokedActions; }

        protected virtual List<IReadOnlyCollection<IEventLogic<T>>> addonEventsCollections => new();

        protected List<IEventLogic<T>> eventsToListen = new();
        public virtual List<IEventLogic<T>> EventsToListen { get; }

        #endregion

        #region Public Methods

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public virtual bool Initialize()
        {
            try
            {
                foreach (IReadOnlyCollection<IEventLogic<T>> collection in addonEventsCollections)
                {
                    foreach (IEventLogic<T> eventLogic in collection)
                    {
                        eventsToListen.Add(eventLogic);
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return false;
            }

            return true;
        }

        public virtual void OnInvoked(T data)
        {
            OnInvokedActions?.Invoke(data);
        }

        public virtual void Subscribe()
        {
            foreach (var events in eventsToListen)
            {
                events.AddListener(this);
            }
        }

        public virtual void UnSubscribe()
        {
            foreach (var events in eventsToListen)
            {
                events.RemoveListener(this);
            }
        }

        public virtual void AddEventToListen(IEventLogic<T> eventLogic, bool updateSubscription = false)
        {
            eventsToListen.Add(eventLogic);

            if (updateSubscription)
                eventLogic.AddListener(this);
        }

        public virtual void RemoveEventToListen(IEventLogic<T> eventLogic, bool updateSubscription = false)
        {
            eventsToListen.Remove(eventLogic);

            if (updateSubscription)
                eventLogic.RemoveListener(this);
        }

        #endregion
    }

}