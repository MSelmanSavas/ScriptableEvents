
using System.Collections.Generic;
using MSS.ScriptableEvents.Events;

namespace MSS.ScriptableEvents.Variables
{
    public interface IScriptableVariable<T>
    {
        T Value { get; set; }
        List<GenericScriptableEvent<T>> OnValueChangedEvents { get; }
    }
}