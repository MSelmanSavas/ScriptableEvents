namespace MSS.ScriptableEvents
{
    public interface IScriptableEventListenerSubscriber : IEventListenerSubscriber
    {
#if UNITY_EDITOR
        void SubscribePersistent();
        void UnSubscribePersistent();
#endif
    }
}