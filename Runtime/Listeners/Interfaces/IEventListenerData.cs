using System.Collections.Generic;
using UnityEngine.Events;
using MSS.ScriptableEvents.Events;

namespace MSS.ScriptableEvents.Listeners
{
    public interface IEventListenerData
    {
        UnityEvent OnInvokedActions { get; }
        List<IEventLogic> EventsToListen { get; }
    }

    public interface IEventListenerData<T>
    {
        UnityEvent<T> OnInvokedActions { get; }
        List<IEventLogic<T>> EventsToListen { get; }
    }
}