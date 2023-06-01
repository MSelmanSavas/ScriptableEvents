using MSS.ScriptableEvents;

[System.Serializable]
public class ScriptableGenericEvent<T> : BaseScriptableEvent<T>
{
    public GenericEvent<T> GenericEvent;
    public override IEventLogic<T> EventLogic => GenericEvent;
    public override IEventInvoker<T> EventInvoker => GenericEvent;
    public override IEventData<T> EventData => GenericEvent;
}
