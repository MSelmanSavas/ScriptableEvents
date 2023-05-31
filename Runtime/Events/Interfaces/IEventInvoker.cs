

namespace MSS.ScriptableEvents
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