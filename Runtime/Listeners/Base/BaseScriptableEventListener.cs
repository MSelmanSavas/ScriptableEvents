using System.Collections.Generic;
using MSS.ScriptableEvents.Events;
using UnityEngine;
using UnityEngine.Events;

namespace MSS.ScriptableEvents.Listeners
{
    [System.Serializable]
    public abstract class BaseScriptableEventListener : ScriptableObject,
                                                            IScriptableEventListenerLogic,
                                                            IScriptableEventListenerSubscriber,
                                                            IScriptableEventListenerInvoker
    {
        #region Members

        [System.NonSerialized]
        bool _isSubscribed = false;
        public bool IsAlreadySubscribed => _isSubscribed;

        [System.NonSerialized]
        EnumAutoActivationMode _previousActivationMode;

        [SerializeField]
        protected EnumAutoActivationMode activationMode;
        public EnumAutoActivationMode ActivationMode => activationMode;

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
                scriptableEvent?.AddListener(this);
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
                scriptableEvent?.RemoveListener(this);
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
                scriptableEvent?.AddPersistentListener(this);
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
                scriptableEvent?.RemovePersistentListener(this);
            }
        }
#endif

        #endregion

        private void OnEnable()
        {
#if UNITY_EDITOR

            UnityEditor.EditorApplication.playModeStateChanged += PlayStateChanged;

#elif !UNITY_EDITOR
            if (activationMode.HasFlag(EnumAutoActivationMode.PlayMode))
            {
                UnSubscribe();
                Subscribe();
            }

            _previousActivationMode = activationMode;
#endif
        }


        private void OnDisable()
        {

#if !UNITY_EDITOR       

            if (activationMode.HasFlag(EnumAutoActivationMode.PlayMode))
            {
                UnSubscribe();
            }

            _previousActivationMode = activationMode;
#endif
        }

#if UNITY_EDITOR

        void PlayStateChanged(UnityEditor.PlayModeStateChange state)
        {
            OnEditorInitialize();

            switch (state)
            {
                case UnityEditor.PlayModeStateChange.EnteredPlayMode:
                    {
                        if (activationMode.HasFlag(EnumAutoActivationMode.PlayMode))
                        {
                            UnSubscribe();
                            Subscribe();
                        }

                        break;
                    }
                case UnityEditor.PlayModeStateChange.ExitingPlayMode:
                    {
                        if (activationMode.HasFlag(EnumAutoActivationMode.PlayMode))
                        {
                            UnSubscribe();
                        }
                        break;
                    }
            }

            _previousActivationMode = activationMode;
        }

        void OnEditorInitialize()
        {
            if (activationMode.HasFlag(EnumAutoActivationMode.EditorMode)
             && !UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
            {
                UnSubscribe();
                Subscribe();
            }

            if (_previousActivationMode.HasFlag(EnumAutoActivationMode.EditorMode)
            && !activationMode.HasFlag(EnumAutoActivationMode.EditorMode))
            {
                UnSubscribe();
            }

            if (activationMode.HasFlag(EnumAutoActivationMode.EditorMode)
            && !activationMode.HasFlag(EnumAutoActivationMode.PlayMode)
            && UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
            {
                UnSubscribe();
            }

            _previousActivationMode = activationMode;
        }

        private void OnValidate()
        {
            OnEditorInitialize();
        }
#endif

    }

    public abstract class BaseScriptableEventListener<T> : ScriptableObject,
                                                            IScriptableEventListenerLogic<T>,
                                                            IScriptableEventListenerSubscriber,
                                                            IScriptableEventListenerInvoker<T>
    {
        #region Members

        [System.NonSerialized]
        bool _isSubscribed = false;
        public bool IsAlreadySubscribed => _isSubscribed;

        [System.NonSerialized]
        EnumAutoActivationMode _previousActivationMode;

        [SerializeField]
        protected EnumAutoActivationMode activationMode;
        public EnumAutoActivationMode ActivationMode => activationMode;

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
                scriptableEvent?.AddListener(this);
            }

            _isSubscribed = true;
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        public void UnSubscribe()
        {
            foreach (var scriptableEvent in _scriptableEventsToListen)
            {
                scriptableEvent?.RemoveListener(this);
            }

            _isSubscribed = false;
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
                scriptableEvent?.AddPersistentListener(this);
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
                scriptableEvent?.RemovePersistentListener(this);
            }
        }
#endif

        #endregion

        private void OnEnable()
        {
#if UNITY_EDITOR

            UnityEditor.EditorApplication.playModeStateChanged += PlayStateChanged;

#elif !UNITY_EDITOR
            if (activationMode.HasFlag(EnumAutoActivationMode.PlayMode))
            {
                UnSubscribe();
                Subscribe();
            }

            _previousActivationMode = activationMode;
#endif
        }


        private void OnDisable()
        {

#if !UNITY_EDITOR       

            if (activationMode.HasFlag(EnumAutoActivationMode.PlayMode))
            {
                UnSubscribe();
            }

            _previousActivationMode = activationMode;
#endif
        }

#if UNITY_EDITOR


        void PlayStateChanged(UnityEditor.PlayModeStateChange state)
        {
            OnEditorInitialize();

            switch (state)
            {
                case UnityEditor.PlayModeStateChange.EnteredPlayMode:
                    {
                        if (activationMode.HasFlag(EnumAutoActivationMode.PlayMode))
                        {
                            UnSubscribe();
                            Subscribe();
                        }

                        break;
                    }
                case UnityEditor.PlayModeStateChange.ExitingPlayMode:
                    {
                        if (activationMode.HasFlag(EnumAutoActivationMode.PlayMode))
                        {
                            UnSubscribe();
                        }
                        break;
                    }
            }

            _previousActivationMode = activationMode;
        }

        void OnEditorInitialize()
        {
            if (activationMode.HasFlag(EnumAutoActivationMode.EditorMode)
             && !UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
            {
                UnSubscribe();
                Subscribe();
            }

            if (_previousActivationMode.HasFlag(EnumAutoActivationMode.EditorMode)
            && !activationMode.HasFlag(EnumAutoActivationMode.EditorMode))
            {
                UnSubscribe();
            }

            if (activationMode.HasFlag(EnumAutoActivationMode.EditorMode)
            && !activationMode.HasFlag(EnumAutoActivationMode.PlayMode)
            && UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
            {
                UnSubscribe();
            }

            _previousActivationMode = activationMode;
        }

        private void OnValidate()
        {
            OnEditorInitialize();
        }
#endif

    }

}