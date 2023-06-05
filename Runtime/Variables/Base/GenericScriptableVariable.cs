using System.Collections.Generic;
using MSS.ScriptableEvents.Events;
using UnityEngine;

namespace MSS.ScriptableEvents.Variables
{
    [System.Serializable]
    public class GenericScriptableVariable<T> : ScriptableObject, IVariable<T>
    {

#if UNITY_EDITOR
        #region Only Editor Fields and Methods

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
#endif
        protected bool showEditorUtilities;

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowIf("showEditorUtilities")]
        [Sirenix.OdinInspector.Button]
#endif
        protected void InvokeForEditor()
        {
            Value = Value;
        }

        #endregion
#endif

        public GenericVariable<T> Variable = new();

        public T Value { get => Variable.Value; set => Variable.Value = value; }
        public List<GenericScriptableEvent<T>> OnValueChangedEvents => Variable.OnValueChangedEvents;
    }
}
