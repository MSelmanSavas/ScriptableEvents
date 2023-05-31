using UnityEngine;
using MSS.ScriptableEvents;

[System.Serializable]
[CreateAssetMenu(fileName = "NewIntEvent", menuName = "ScriptableEvents/Events/Create Int Event")]
public class ScriptableIntEvent : BaseScriptableEvent<int>
{
    [SerializeField]
    protected IntEvent _intEvent = new();
    public override IEventLogic<int> EventLogic => _intEvent;
    public override IEventInvoker<int> EventInvoker => _intEvent;
}
