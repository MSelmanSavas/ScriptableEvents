using System.Collections.Generic;
using UnityEngine.Events;

namespace MSS.ScriptableEvents
{
    public interface IScriptableEventListenerData
    {
        UnityEvent OnInvokedActions { get; }
        List<BaseScriptableEvent> ScriptableEventsToListen { get; }
    }
    public interface IScriptableEventListenerData<T>
    {
        UnityEvent<T> OnInvokedActions { get; }
        List<BaseScriptableEvent<T>> ScriptableEventsToListen { get; }
    }
}