using UnityEngine;

namespace MSS.ScriptableEvents.Editor
{

#if ODIN_INSPECTOR

    [UnityEditor.CustomEditor(typeof(BaseScriptableEventListener), editorForChildClasses: true)]
    public class BaseScriptableEventListenerDrawer : Sirenix.OdinInspector.Editor.OdinEditor
    {
        public override void OnInspectorGUI()
        {
            if (Sirenix.OdinInspector.Editor.InspectorConfig.Instance.EnableOdinInInspector)
            {
                base.OnInspectorGUI();
                return;
            }

            DrawUnityInspector();

            if (GUILayout.Button("On Invoked"))
            {
                if (target is BaseScriptableEventListener scriptableBaseEvent)
                    scriptableBaseEvent.OnInvoked();
            }

            if (GUILayout.Button("Subscribe"))
            {
                if (target is BaseScriptableEventListener scriptableBaseEvent)
                    scriptableBaseEvent.Subscribe();
            }

            if (GUILayout.Button("UnSubscribe"))
            {
                if (target is BaseScriptableEventListener scriptableBaseEvent)
                    scriptableBaseEvent.UnSubscribe();
            }
        }
    }

#elif !ODIN_INSPECTOR

    [UnityEditor.CustomEditor(typeof(BaseScriptableEventListener), editorForChildClasses: true)]
    public class BaseScriptableEventListenerDrawer : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawUnityInspector();

            if (GUILayout.Button("Invoke Listener"))
            {
                if (target is BaseScriptableEventListener scriptableBaseEvent)
                    scriptableBaseEvent.Invoke();
            }

            if (GUILayout.Button("Subscribe"))
            {
                if (target is BaseScriptableEventListener scriptableBaseEvent)
                    scriptableBaseEvent.Subscribe();
            }

            if (GUILayout.Button("UnSubscribe"))
            {
                if (target is BaseScriptableEventListener scriptableBaseEvent)
                    scriptableBaseEvent.UnSubscribe();
            }
        }
    }

#endif
}


