using System.Collections.Generic;
using MSS.ScriptableEvents.Events;
using UnityEngine;

namespace MSS.ScriptableEvents.Variables
{
    [System.Serializable]
    public class GenericScriptableVariable<T> : ScriptableObject, IVariable<T>
    {
        public GenericVariable<T> Variable = new();

        public T Value { get => Variable.Value; set => Variable.Value = value; }
        public List<GenericScriptableEvent<T>> OnValueChangedEvents => Variable.OnValueChangedEvents;
    }
}
