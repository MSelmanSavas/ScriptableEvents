using UnityEngine;
using MSS.ScriptableEvents.Events;

[System.Serializable]
[CreateAssetMenu(fileName = "NewIntEvent", menuName = "ScriptableEvents/Events/Create Int Event")]
public class ScriptableIntEvent : ScriptableGenericEvent<int>
{

}