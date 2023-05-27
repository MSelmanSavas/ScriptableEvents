using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScriptableBaseEvent), editorForChildClasses: true)]
public class ScriptableBaseEventDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Invoke Event"))
        {
            Debug.LogError(target);
            Debug.LogError(target.GetType());
            Debug.LogError(serializedObject);
            Debug.LogError(serializedObject.FindProperty("Invoke"));

            Debug.LogError(target as ScriptableBaseEvent);
            Debug.LogError(target is ScriptableBaseEvent);

            if (target is ScriptableBaseEvent scriptableBaseEvent)
                scriptableBaseEvent.Invoke();
        }
    }
}
