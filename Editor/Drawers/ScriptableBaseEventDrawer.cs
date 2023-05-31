
using UnityEngine;

namespace MSS.ScriptableEvents.Editor
{

#if ODIN_INSPECTOR

    [UnityEditor.CustomEditor(typeof(BaseScriptableEvent), editorForChildClasses: true)]
    public class ScriptableBaseEventDrawer : Sirenix.OdinInspector.Editor.OdinEditor
    {
        public override void OnInspectorGUI()
        {
            if (Sirenix.OdinInspector.Editor.InspectorConfig.Instance.EnableOdinInInspector)
            {
                base.OnInspectorGUI();
                return;
            }

            DrawUnityInspector();

            if (GUILayout.Button("Invoke Event"))
            {
                if (target is BaseScriptableEvent scriptableBaseEvent)
                    scriptableBaseEvent.Invoke();
            }
        }
    }

#elif !ODIN_INSPECTOR

    [UnityEditor.CustomEditor(typeof(BaseScriptableEvent), editorForChildClasses: true)]
    public class ScriptableBaseEventDrawer : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Invoke Event"))
            {
                if (target is BaseScriptableEvent scriptableBaseEvent)
                    scriptableBaseEvent.Invoke();
            }
        }
    }
    
#endif
}

