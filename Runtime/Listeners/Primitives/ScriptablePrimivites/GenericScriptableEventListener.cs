using UnityEngine;

namespace MSS.ScriptableEvents.Listeners
{
    public class GenericScriptableEventListener<T> : BaseScriptableEventListener<T>
    {
        [SerializeField]
        protected GenericEventListener<T> _eventListener = new();
        public override IEventListenerLogic<T> OnInvokedLogic => _eventListener;
        public override IEventListenerData<T> OnInvokedData => _eventListener;
        public override IEventListenerInvoker<T> OnInvokedActions => _eventListener;
    }

}
