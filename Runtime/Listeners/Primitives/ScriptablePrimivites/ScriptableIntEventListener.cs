using MSS.ScriptableEvents;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewIntListener", menuName = "ScriptableEvents/Listeners/Create Int Listener")]
public class ScriptableIntEventListener : BaseScriptableEventListener<int>
{

    public void Test(int value)
    {
        Debug.LogError($"Horsey : {value}");
    }
}
