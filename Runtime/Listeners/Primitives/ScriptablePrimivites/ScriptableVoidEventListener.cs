using MSS.ScriptableEvents;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewVoidListener", menuName = "ScriptableEvents/Listeners/Create VoidListener")]
public class ScriptableVoidEventListener : ScriptableBaseListener
{
    [SerializeField]
    VoidEventListenerScriptable _listener = new();
    public override IScriptableEventListener Listener => _listener;

    public void Ass()
    {
        Debug.LogError("Ass");
    }
}