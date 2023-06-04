using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MSS.ScriptableEvents
{
    [System.Serializable]
    public abstract class BaseScriptableEventListener : ScriptableObject,
                                                            IScriptableEventListenerLogic,
                                                            IScriptableEventListenerSubscriber,
                                                            IScriptableEventListenerInvoker
    {
        #region Members

        public abstract IEventListenerLogic OnInvokedLogic { get; }
        public abstract IEventListenerData OnInvokedData { get; }
        public abstract IEventListenerInvoker OnInvokedActions { get; }

#if UNITY_EDITOR
        #region Only Editor Fields and Methods

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
#endif
        protected bool showEditorUtilities;

        #endregion
#endif


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
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public void OnInvoked()
        {
            OnInvokedActions?.OnInvoked();
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
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
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public void UnSubscribe()
        {
            foreach (var scriptableEvent in _scriptableEventsToListen)
            {
                scriptableEvent.RemoveListener(this);
            }
        }

#if UNITY_EDITOR

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public void SubscribePersistent()
        {
            foreach (var scriptableEvent in _scriptableEventsToListen)
            {
                scriptableEvent.AddPersistentListener(this);
            }
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public void UnSubscribePersistent()
        {
            foreach (var scriptableEvent in _scriptableEventsToListen)
            {
                scriptableEvent.RemovePersistentListener(this);
            }
        }
#endif

        #endregion
    }

    public abstract class BaseScriptableEventListener<T> : ScriptableObject,
                                                            IScriptableEventListenerLogic<T>,
                                                            IScriptableEventListenerSubscriber,
                                                            IScriptableEventListenerInvoker<T>
    {
        #region Members

        public abstract IEventListenerLogic<T> OnInvokedLogic { get; }
        public abstract IEventListenerData<T> OnInvokedData { get; }
        public abstract IEventListenerInvoker<T> OnInvokedActions { get; }

#if UNITY_EDITOR
        #region Only Editor Fields and Methods


#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
#endif
        protected bool showEditorUtilities;

        [SerializeField]
        [HideInInspector]
        protected T editorData;

        public virtual void InvokeForEditor()
        {
            OnInvokedActions?.OnInvoked(editorData);
        }

        #endregion
#endif

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
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public void OnInvoked(T data)
        {
            OnInvokedActions?.OnInvoked(data);
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
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
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public void UnSubscribe()
        {
            foreach (var scriptableEvent in _scriptableEventsToListen)
            {
                scriptableEvent.RemoveListener(this);
            }
        }

#if UNITY_EDITOR

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public void SubscribePersistent()
        {
            foreach (var scriptableEvent in _scriptableEventsToListen)
            {
                scriptableEvent.AddPersistentListener(this);
            }
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public void UnSubscribePersistent()
        {
            foreach (var scriptableEvent in _scriptableEventsToListen)
            {
                scriptableEvent.RemovePersistentListener(this);
            }
        }
#endif

        #endregion
    }

}