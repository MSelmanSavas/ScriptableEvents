using MSS.ScriptableEvents.Listeners;
using UnityEngine;

namespace MSS.ScriptableEvents.Events
{
    [System.Serializable]
    public abstract class BaseScriptableEvent : ScriptableObject, IEventLogic, IScriptableEventInvoker
    {

#if UNITY_EDITOR
        #region Only Editor Fields and Methods

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
#endif
        protected bool showEditorUtilities;

        #endregion
#endif

        public abstract IEventData EventData { get; }
        public abstract IEventLogic EventLogic { get; }
        public abstract IEventInvoker EventInvoker { get; }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public virtual void Invoke()
        {
            EventInvoker.Invoke();
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public virtual void AddListener(IEventListenerInvoker listener)
        {
            EventLogic.AddListener(listener);
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public virtual void RemoveListener(IEventListenerInvoker listener)
        {
            EventLogic.RemoveListener(listener);
        }

#if UNITY_EDITOR

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public void AddPersistentListener(IEventListenerInvoker listener)
        {
            UnityEditor.Events.UnityEventTools.AddPersistentListener(EventData.Action, listener.OnInvoked);
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public void RemovePersistentListener(IEventListenerInvoker listener)
        {
            UnityEditor.Events.UnityEventTools.RemovePersistentListener(EventData.Action, listener.OnInvoked);
        }
#endif
    }

    [System.Serializable]
    public abstract class BaseScriptableEvent<T> : ScriptableObject, IEventLogic<T>, IScriptableEventInvoker<T>
    {
        public abstract IEventData<T> EventData { get; }
        public abstract IEventLogic<T> EventLogic { get; }
        public abstract IEventInvoker<T> EventInvoker { get; }

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
            EventInvoker.Invoke(editorData);
        }

        #endregion
#endif

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public virtual void Invoke(T data)
        {
            EventInvoker.Invoke(data);
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public virtual void AddListener(IEventListenerInvoker<T> listener)
        {
            EventLogic.AddListener(listener);
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public virtual void RemoveListener(IEventListenerInvoker<T> listener)
        {
            EventLogic.RemoveListener(listener);
        }

#if UNITY_EDITOR

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public void AddPersistentListener(IEventListenerInvoker<T> listener)
        {
            UnityEditor.Events.UnityEventTools.AddPersistentListener<T>(EventData.Action, listener.OnInvoked);
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public void RemovePersistentListener(IEventListenerInvoker<T> listener)
        {
            UnityEditor.Events.UnityEventTools.RemovePersistentListener<T>(EventData.Action, listener.OnInvoked);
        }
#endif
    }
}
