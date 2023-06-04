namespace MSS.ScriptableEvents.Events
{
    public interface IEventInvoker
    {
        void Invoke();
    }

    public interface IEventInvoker<T>
    {
        void Invoke(T data);
    }
}