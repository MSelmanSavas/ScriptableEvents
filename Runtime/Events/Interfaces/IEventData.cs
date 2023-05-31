using UnityEngine.Events;

namespace MSS.ScriptableEvents
{
    public interface IEventData
    {
        public UnityAction Action { get; }
    }

    public interface IEventData<T>
    {
        public UnityAction<T> Action { get; }
    }
}
