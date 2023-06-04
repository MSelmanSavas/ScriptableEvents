namespace MSS.ScriptableEvents.Listeners
{
    public interface IEventListenerInvoker
    {
        void OnInvoked();
    }

    public interface IEventListenerInvoker<T>
    {
        void OnInvoked(T data);
    }
}