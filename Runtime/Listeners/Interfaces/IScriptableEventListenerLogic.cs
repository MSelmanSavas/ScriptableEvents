using MSS.ScriptableEvents.Events;

namespace MSS.ScriptableEvents.Listeners
{
    public interface IScriptableEventListenerLogic
    {
        void AddScriptableEventToListen(BaseScriptableEvent scriptableBaseEvent, bool updateSubscription = false);
        void RemoveScriptableEventToLister(BaseScriptableEvent scriptableBaseEvent, bool updateSubscription = false);
    }

    public interface IScriptableEventListenerLogic<T>
    {
        void AddScriptableEventToListen(BaseScriptableEvent<T> scriptableBaseEvent, bool updateSubscription = false);
        void RemoveScriptableEventToLister(BaseScriptableEvent<T> scriptableBaseEvent, bool updateSubscription = false);
    }
}