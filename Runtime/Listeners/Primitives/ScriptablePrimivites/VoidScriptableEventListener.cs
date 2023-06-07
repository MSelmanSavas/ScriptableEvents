using MSS.ScriptableEvents.Listeners;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewVoidListener", menuName = "ScriptableEvents/Listeners/Create Void Listener")]
public class VoidScriptableEventListener : BaseScriptableEventListener
{
    [SerializeField]
    protected VoidEventListener _eventListener = new();
    public override IEventListenerLogic OnInvokedLogic => _eventListener;
    public override IEventListenerData OnInvokedData => _eventListener;
    public override IEventListenerInvoker OnInvokedActions => _eventListener;
}