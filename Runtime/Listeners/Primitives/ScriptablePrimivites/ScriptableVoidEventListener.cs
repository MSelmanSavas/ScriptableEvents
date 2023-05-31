using MSS.ScriptableEvents;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewVoidListener", menuName = "ScriptableEvents/Listeners/Create VoidListener")]
public class ScriptableVoidEventListener : BaseScriptableEventListener
{
    public void Test()
    {
        Debug.LogError("Horsey");
    }
}