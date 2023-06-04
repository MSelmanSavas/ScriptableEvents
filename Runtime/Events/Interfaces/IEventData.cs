using UnityEngine.Events;

namespace MSS.ScriptableEvents.Events
{
    public interface IEventData
    {
        public UnityEvent Action { get; }
    }

    public interface IEventData<T>
    {
        public UnityEvent<T> Action { get; }
    }
}
