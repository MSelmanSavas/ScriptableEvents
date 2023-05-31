using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MSS.ScriptableEvents
{
    [System.Serializable]
    public abstract class BaseScriptableEventListener : ScriptableObject,
                                                            IScriptableEventListenerData,
                                                            IScriptableEventListenerLogic,
                                                            IEventListenerSubscriber,
                                                            IEventListenerInvoker
    {
        #region Members

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
#endif
        [SerializeField]
        protected UnityEvent _actions = new();

        public UnityEvent Actions { get => _actions; }


#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
#endif
        [SerializeField]
        protected List<BaseScriptableEvent> _scriptableEventsToListen = new();

        public virtual List<BaseScriptableEvent> ScriptableEventsToListen => _scriptableEventsToListen;

        #endregion

        #region Public Methods

        public void AddScriptableEventToListen(BaseScriptableEvent scriptableBaseEvent, bool updateSubscription = false)
        {
            _scriptableEventsToListen.Add(scriptableBaseEvent);

            if (updateSubscription)
                scriptableBaseEvent.EventLogic.AddListener(this);
        }

        public void RemoveScriptableEventToLister(BaseScriptableEvent scriptableBaseEvent, bool updateSubscription = false)
        {
            _scriptableEventsToListen.Remove(scriptableBaseEvent);

            if (updateSubscription)
                scriptableBaseEvent.EventLogic.RemoveListener(this);
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void OnInvoked()
        {
            Actions?.Invoke();
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void Subscribe()
        {
            foreach (var scriptableEvent in _scriptableEventsToListen)
            {
                scriptableEvent.AddListener(this);
            }
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void UnSubscribe()
        {
            foreach (var scriptableEvent in _scriptableEventsToListen)
            {
                scriptableEvent.RemoveListener(this);
            }
        }

        #endregion
    }

    public abstract class BaseScriptableEventListener<T> : ScriptableObject,
                                                            IScriptableEventListenerData<T>,
                                                            IScriptableEventListenerLogic<T>,
                                                            IEventListenerSubscriber,
                                                            IEventListenerInvoker<T>
    {
        #region Members

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
#endif
        [SerializeField]
        protected UnityEvent<T> _actions = new();

        public UnityEvent<T> Actions { get => _actions; }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
#endif
        [SerializeField]
        protected List<BaseScriptableEvent<T>> _scriptableEventsToListen = new();

        public virtual List<BaseScriptableEvent<T>> ScriptableEventsToListen => _scriptableEventsToListen;

        #endregion

        #region Public Methods

        public void AddScriptableEventToListen(BaseScriptableEvent<T> scriptableBaseEvent, bool updateSubscription = false)
        {
            _scriptableEventsToListen.Add(scriptableBaseEvent);

            if (updateSubscription)
                scriptableBaseEvent.EventLogic.AddListener(this);
        }

        public void RemoveScriptableEventToLister(BaseScriptableEvent<T> scriptableBaseEvent, bool updateSubscription = false)
        {
            _scriptableEventsToListen.Remove(scriptableBaseEvent);

            if (updateSubscription)
                scriptableBaseEvent.EventLogic.RemoveListener(this);
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void OnInvoked(T data)
        {
            Actions?.Invoke(data);
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void Subscribe()
        {
            foreach (var scriptableEvent in _scriptableEventsToListen)
            {
                scriptableEvent.AddListener(this);
            }
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void UnSubscribe()
        {
            foreach (var scriptableEvent in _scriptableEventsToListen)
            {
                scriptableEvent.RemoveListener(this);
            }
        }

        #endregion
    }

}