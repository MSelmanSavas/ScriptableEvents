using System.Collections.Generic;
using MSS.ScriptableEvents.Events;
using UnityEngine;

namespace MSS.ScriptableEvents.Listeners
{
    [System.Serializable]
    public class GenericEventListener<T> : BaseEventListener<T>
    {
        [SerializeField]
        protected List<GenericScriptableEvent<T>> scriptableEventsToListen = new();

        protected override List<IReadOnlyCollection<IEventLogic<T>>> addonEventsCollections
        {
            get
            {
                var baseAddons = base.addonEventsCollections;
                baseAddons.Add(scriptableEventsToListen);
                return baseAddons;
            }
        }
    }
}
