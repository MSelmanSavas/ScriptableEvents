namespace MSS.ScriptableEvents
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