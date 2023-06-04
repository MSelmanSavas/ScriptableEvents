using System.Collections.Generic;
using UnityEngine.Events;

namespace MSS.ScriptableEvents
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