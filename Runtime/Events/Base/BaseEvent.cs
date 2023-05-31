using UnityEngine;
using UnityEngine.Events;

namespace MSS.ScriptableEvents
{
    [System.Serializable]
    public abstract class BaseEvent : IEventData, IEventInvoker, IEventLogic
    {
        #region Members

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
#endif
        [SerializeField]
        protected event UnityAction _onInvoke;

        public UnityAction Action => _onInvoke;

        #endregion

        #region Public Methods

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void Invoke()
        {
            _onInvoke?.Invoke();
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void AddListener(IEventListenerInvoker listener)
        {
            _onInvoke += listener.OnInvoked;
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void RemoveListener(IEventListenerInvoker listener)
        {
            _onInvoke -= listener.OnInvoked;
        }

        #endregion
    }

    [System.Serializable]
    public abstract class BaseEvent<T> : IEventData<T>, IEventInvoker<T>, IEventLogic<T>
    {
        #region Members

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
#endif
        [SerializeField]
        protected event UnityAction<T> _onInvoke;

        public UnityAction<T> Action => _onInvoke;

        #endregion

        #region Public Methods

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void Invoke(T data)
        {
            _onInvoke?.Invoke(data);
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void AddListener(IEventListenerInvoker<T> listener)
        {
            _onInvoke += listener.OnInvoked;
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void RemoveListener(IEventListenerInvoker<T> listener)
        {
            _onInvoke -= listener.OnInvoked;
        }

        #endregion
    }
}