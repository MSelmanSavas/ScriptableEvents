namespace MSS.ScriptableEvents
{
    public interface IScriptableEventLogic : IEventLogic
    {
#if UNITY_EDITOR
        void AddPersistentListener(IEventListenerInvoker listener);
        void RemovePersistentListener(IEventListenerInvoker listener);
#endif
    }

    public interface IScriptableEventLogic<T> : IEventLogic<T>
    {

#if UNITY_EDITOR
        void AddPersistentListener(IEventListenerInvoker<T> listener);
        void RemovePersistentListener(IEventListenerInvoker<T> listener);
#endif
    }
}