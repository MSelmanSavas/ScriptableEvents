namespace MSS.ScriptableEvents
{
    public interface IEventLogic
    {
        void AddListener(IEventInvoker listener);
        void RemoveListener(IEventInvoker listener);
    }

    public interface IEventLogic<T>
    {
        void AddListener(IEventInvoker<T> listener);
        void RemoveListener(IEventInvoker<T> listener);
    }

}