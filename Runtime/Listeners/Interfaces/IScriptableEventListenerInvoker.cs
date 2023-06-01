
namespace MSS.ScriptableEvents
{
    public interface IScriptableEventListenerInvoker : IEventListenerInvoker
    {

    }

    public interface IScriptableEventListenerInvoker<T> : IEventListenerInvoker<T>
    {
#if UNITY_EDITOR
        void InvokeForEditor();
#endif
    }
}
