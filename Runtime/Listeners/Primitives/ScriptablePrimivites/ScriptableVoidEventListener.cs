using MSS.ScriptableEvents;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewVoidListener", menuName = "ScriptableEvents/Listeners/Create Void Listener")]
public class ScriptableVoidEventListener : BaseScriptableEventListener
{
    [SerializeField]
    protected VoidEventListener _eventListener = new();
    public override IEventListenerLogic OnInvokedLogic => _eventListener;
    public override IEventListenerData OnInvokedData => _eventListener;
    public override IEventListenerInvoker OnInvokedActions => _eventListener;

    public void Test()
    {
        Debug.LogError("Horsey");
    }
}