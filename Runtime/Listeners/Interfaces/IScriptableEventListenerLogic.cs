namespace MSS.ScriptableEvents
{
    public interface IScriptableEventListenerLogic
    {
        void AddScriptableEventToListen(ScriptableBaseEvent scriptableBaseEvent, bool updateSubscription = false);
        void RemoveScriptableEventToLister(ScriptableBaseEvent scriptableBaseEvent, bool updateSubscription = false);
    }

    public interface IScriptableEventListenerLogic<T>
    {
        void AddScriptableEventToListen(ScriptableBaseEvent<T> scriptableBaseEvent, bool updateSubscription = false);
        void RemoveScriptableEventToLister(ScriptableBaseEvent<T> scriptableBaseEvent, bool updateSubscription = false);
    }
}