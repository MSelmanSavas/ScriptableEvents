namespace MSS.ScriptableEvents
{
    public interface IEventListenerLogic
    {
        void AddEventToListen(IEventLogic baseEvent, bool updateSubscription = false);
        void RemoveEventToListen(IEventLogic baseEvent, bool updateSubscription = false);
    }

    public interface IEventListenerLogic<T>
    {
        void AddEventToListen(IEventLogic<T> baseEvent, bool updateSubscription = false);
        void RemoveEventToListen(IEventLogic<T> baseEvent, bool updateSubscription = false);
    }
}