using MSS.ScriptableEvents.Events;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewStringEvent", menuName = "ScriptableEvents/Events/Create String Event")]
public class StringScriptableEvent : GenericScriptableEvent<string>
{

}
