using MSS.ScriptableEvents.Events;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewIntEvent", menuName = "ScriptableEvents/Events/Create Int Event")]
public class ScriptableIntEvent : ScriptableGenericEvent<int>
{

}