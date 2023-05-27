using MSS.ScriptableEvents;
using UnityEngine;

[CreateAssetMenu(fileName = "NewVoidEvent", menuName = "ScriptableEvents/Events/Create VoidEvent")]
public class ScriptableVoidEvent : ScriptableBaseEvent
{
    [SerializeField]
    protected VoidEvent _voidEvent = new();
    public override IEvent Event => _voidEvent;
}
