using MSS.ScriptableEvents;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewVoidListener", menuName = "ScriptableEvents/Listeners/Create VoidListener")]
public class ScriptableVoidEventListener : ScriptableBaseListener
{
    [SerializeField]
    VoidEventListenerScriptable _listener = new();
    public override IEventListenerSubsriber Listener => _listener;

    public void Test()
    {
        Debug.LogError("Here test 122231");
    }
}