using System.Collections.Generic;
using MSS.ScriptableEvents.Events;
using UnityEngine;
using UnityEngine.Events;

namespace MSS.ScriptableEvents.Listeners
{
    [System.Serializable]
    public class GenericMonoScriptableEventListener<T> : MonoBehaviour, IEventListenerInvoker<T>, IEventListenerSubscriber
    {
        [SerializeField]
        UnityEvent<T> _onInvokedActions = new();

        [SerializeField]
        protected List<BaseScriptableEvent<T>> _eventsToListen = new();

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        public virtual void OnInvoked(T data)
        {
            _onInvokedActions?.Invoke(data);
        }

        public virtual void Subscribe()
        {
            foreach (var scriptableEvent in _eventsToListen)
            {
                scriptableEvent.AddListener(this);
            }
        }

        public virtual void UnSubscribe()
        {
            foreach (var scriptableEvent in _eventsToListen)
            {
                scriptableEvent.RemoveListener(this);
            }
        }
    }
}

