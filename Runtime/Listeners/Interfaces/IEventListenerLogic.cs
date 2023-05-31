namespace MSS.ScriptableEvents
{
    public interface IEventListenerLogic
    {
        void AddEventToListen(IEventLogic baseEvent, bool updateSubscription = false);
        void RemoveEventToLister(IEventLogic baseEvent, bool updateSubscription = false);
    }

    public interface IEventListenerLogic<T>
    {
        void AddEventToListen(IEventLogic<T> baseEvent, bool updateSubscription = false);
        void RemoveEventToLister(IEventLogic<T> baseEvent, bool updateSubscription = false);
    }
}