using System.Collections.Generic;
using UnityEngine.Events;

namespace MSS.ScriptableEvents
{

    public interface IEventListenerData
    {
        UnityEvent Actions { get; }
        List<IEventLogic> EventsToListen { get; }
    }

    public interface IEventListenerData<T>
    {
        UnityEvent<T> Actions { get; }
        List<IEventLogic<T>> EventsToListen { get; }
    }
}