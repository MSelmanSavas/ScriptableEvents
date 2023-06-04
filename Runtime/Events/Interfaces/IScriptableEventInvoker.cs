namespace MSS.ScriptableEvents.Events
{
    public interface IScriptableEventInvoker : IEventInvoker
    {

    }

    public interface IScriptableEventInvoker<T> : IEventInvoker<T>
    {
#if UNITY_EDITOR
        void InvokeForEditor();
#endif
    }
}