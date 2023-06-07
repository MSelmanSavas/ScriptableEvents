namespace MSS.ScriptableEvents.Events
{
    [System.Serializable]
    public class GenericScriptableEvent<T> : BaseScriptableEvent<T>
    {
        public GenericEvent<T> GenericEvent = new();
        public override IEventLogic<T> EventLogic => GenericEvent;
        public override IEventInvoker<T> EventInvoker => GenericEvent;
        public override IEventData<T> EventData => GenericEvent;
    }
}
