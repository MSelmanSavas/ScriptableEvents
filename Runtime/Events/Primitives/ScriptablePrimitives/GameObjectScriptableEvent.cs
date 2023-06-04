using MSS.ScriptableEvents.Events;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewGameObjectEvent", menuName = "ScriptableEvents/Events/Create GameObject Event")]
public class GameObjectScriptableEvent : GenericScriptableEvent<GameObject>
{

}
