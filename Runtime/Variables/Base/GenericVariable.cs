using System.Collections.Generic;
using MSS.ScriptableEvents.Events;
using UnityEngine;

namespace MSS.ScriptableEvents.Variables
{
    [System.Serializable]
    public class GenericVariable<T> : IVariable<T>, ISerializationCallbackReceiver
    {
        #region Fields

        [SerializeField]
        protected T initialValue;

        [System.NonSerialized]
        protected T currentValue;

        public virtual T Value
        {
            get => currentValue;
            set
            {
                this.currentValue = value;
                OnValueChanged(this.currentValue);
            }
        }

        [SerializeField]
        protected List<GenericScriptableEvent<T>> onValueChangedEvents = new();
        public virtual List<GenericScriptableEvent<T>> OnValueChangedEvents { get => onValueChangedEvents; set => onValueChangedEvents = value; }

        #endregion

        #region Methods
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        protected virtual void OnValueChanged(T value)
        {
            foreach (var scriptableEvent in OnValueChangedEvents)
                scriptableEvent.Invoke(value);
        }

        public void OnAfterDeserialize()
        {
            currentValue = initialValue;
        }

        public void OnBeforeSerialize() { }
        #endregion
    }
}