using System.Collections.Generic;
using MSS.ScriptableEvents.Events;
using UnityEngine;
using UnityEngine.Events;

namespace MSS.ScriptableEvents.Listeners
{
    [System.Serializable]
    public class VoidMonoScriptableEventListener : MonoBehaviour, IEventListenerInvoker, IEventListenerSubscriber
    {
        [SerializeField]
        UnityEvent _onInvokedActions = new();

        [SerializeField]
        protected List<BaseScriptableEvent> _eventsToListen = new();

        public virtual void OnInvoked()
        {
            _onInvokedActions?.Invoke();
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
