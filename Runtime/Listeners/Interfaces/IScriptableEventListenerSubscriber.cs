namespace MSS.ScriptableEvents.Listeners
{
    public interface IScriptableEventListenerSubscriber : IEventListenerSubscriber
    {
#if UNITY_EDITOR
        void SubscribePersistent();
        void UnSubscribePersistent();
#endif
    }
}