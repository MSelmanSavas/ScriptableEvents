using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(ScriptableBaseEvent), editorForChildClasses: true)]
public class ScriptableBaseEventDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Invoke Event"))
        {
            if (target is ScriptableBaseEvent scriptableBaseEvent)
                scriptableBaseEvent.Invoke();
        }
    }
}
