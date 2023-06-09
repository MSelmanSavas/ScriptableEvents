using MSS.ScriptableEvents.Listeners;

namespace MSS.ScriptableEvents.Events
{
    public interface IEventLogic
    {
        void AddListener(IEventListenerInvoker listener);
        void RemoveListener(IEventListenerInvoker listener);
    }

    public interface IEventLogic<T>
    {
        void AddListener(IEventListenerInvoker<T> listener);
        void RemoveListener(IEventListenerInvoker<T> listener);
    }

}