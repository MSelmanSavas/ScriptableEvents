
using MSS.ScriptableEvents.Events;

namespace MSS.ScriptableEvents.Variables
{
    public interface IScriptableVariable<T>
    {
        T Value { get; set; }
        BaseEvent<T> OnValueChanged { get; set; }
    }
}