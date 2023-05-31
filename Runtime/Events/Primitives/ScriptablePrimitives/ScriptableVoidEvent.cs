using MSS.ScriptableEvents;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewVoidEvent", menuName = "ScriptableEvents/Events/Create VoidEvent")]
public class ScriptableVoidEvent : BaseScriptableEvent
{
    [SerializeField]
    protected VoidEvent _voidEvent = new();

    public override IEventLogic EventLogic => _voidEvent;
    public override IEventInvoker EventInvoker => _voidEvent;
}
