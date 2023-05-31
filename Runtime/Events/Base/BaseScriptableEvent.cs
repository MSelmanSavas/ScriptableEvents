using MSS.ScriptableEvents;
using UnityEngine;

namespace MSS.ScriptableEvents
{
    [System.Serializable]
    public abstract class BaseScriptableEvent : ScriptableObject, IEventLogic, IEventInvoker
    {
        public abstract IEventLogic EventLogic { get; }
        public abstract IEventInvoker EventInvoker { get; }

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
    public abstract class BaseScriptableEvent<T> : ScriptableObject, IEventLogic<T>, IEventInvoker<T>
    {
        public abstract IEventLogic<T> EventLogic { get; }
        public abstract IEventInvoker<T> EventInvoker { get; }

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
}
