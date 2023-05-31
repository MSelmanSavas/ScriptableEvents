using System.Collections.Generic;
using UnityEngine.Events;

namespace MSS.ScriptableEvents
{
    public interface IScriptableEventListenerData
    {
        UnityEvent Actions { get; }
        List<BaseScriptableEvent> ScriptableEventsToListen { get; }
    }
    public interface IScriptableEventListenerData<T>
    {
        UnityEvent<T> Actions { get; }
        List<BaseScriptableEvent<T>> ScriptableEventsToListen { get; }
    }
}