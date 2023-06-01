
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace MSS.ScriptableEvents.Editor
{
#if ODIN_INSPECTOR

    [UnityEditor.CustomEditor(typeof(BaseScriptableEvent), editorForChildClasses: true)]
    public class BaseScriptableEventDrawer : Sirenix.OdinInspector.Editor.OdinEditor
    {
        public override void OnInspectorGUI()
        {
            if (Sirenix.OdinInspector.Editor.InspectorConfig.Instance.EnableOdinInInspector)
            {
                base.OnInspectorGUI();
                return;
            }

            DrawUnityInspector();

            FieldInfo foundShowEditorUtilities = null;

            foreach (FieldInfo fieldInfo in target.GetType().GetFields(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (!fieldInfo.Name.Equals("showEditorUtilities"))
                    continue;

                foundShowEditorUtilities = fieldInfo;
                break;
            }

            bool showEditorUtilities = (bool)foundShowEditorUtilities.GetValue(target);

            showEditorUtilities = GUILayout.Toggle(showEditorUtilities, "Show Editor Utilities");

            foundShowEditorUtilities.SetValue(target, showEditorUtilities);

            if (showEditorUtilities)
            {
                if (GUILayout.Button("Invoke Event"))
                {
                    if (target is IScriptableEventInvoker scriptableBaseEvent)
                        scriptableBaseEvent.Invoke();
                }
            }
        }
    }

    [UnityEditor.CustomEditor(typeof(BaseScriptableEvent<>), editorForChildClasses: true)]
    public class BaseScriptableEventGenericDrawer : Sirenix.OdinInspector.Editor.OdinEditor
    {
        public override void OnInspectorGUI()
        {
            if (Sirenix.OdinInspector.Editor.InspectorConfig.Instance.EnableOdinInInspector)
            {
                base.OnInspectorGUI();
                return;
            }

            DrawUnityInspector();

            FieldInfo foundShowEditorUtilities = null;

            foreach (FieldInfo fieldInfo in target.GetType().GetFields(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (!fieldInfo.Name.Equals("showEditorUtilities"))
                    continue;

                foundShowEditorUtilities = fieldInfo;
                break;
            }

            bool showEditorUtilities = (bool)foundShowEditorUtilities.GetValue(target);

            showEditorUtilities = GUILayout.Toggle(showEditorUtilities, "Show Editor Utilities");

            foundShowEditorUtilities.SetValue(target, showEditorUtilities);

            if (showEditorUtilities)
            {
                DrawEditorUtilities();
                DrawEditorData();
            }

            void DrawEditorData()
            {
                FieldInfo foundEditorData = null;

                foreach (FieldInfo fieldInfo in target.GetType().GetFields(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (!fieldInfo.Name.Equals("editorData"))
                        continue;

                    foundEditorData = fieldInfo;
                    break;
                }

                SerializedProperty serializedProperty = serializedObject.FindProperty(foundEditorData.Name);

                EditorGUILayout.PropertyField(serializedProperty, true);
                serializedObject.ApplyModifiedProperties();
            }

            void DrawEditorUtilities()
            {

                MethodInfo foundInvokeForEditorMethodInfo = null;

                foreach (MethodInfo methodInfo in target.GetType().GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (!methodInfo.Name.Equals("InvokeForEditor"))
                        continue;

                    foundInvokeForEditorMethodInfo = methodInfo;
                    break;
                }

                if (GUILayout.Button("Invoke Event"))
                {
                    foundInvokeForEditorMethodInfo.Invoke(target, null);
                }
            }
        }
    }

#elif !ODIN_INSPECTOR

    [UnityEditor.CustomEditor(typeof(BaseScriptableEvent), editorForChildClasses: true)]
    public class BaseScriptableEventDrawer : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            FieldInfo foundShowEditorUtilities = null;

            foreach (FieldInfo fieldInfo in target.GetType().GetFields(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (!fieldInfo.Name.Equals("showEditorUtilities"))
                    continue;

                foundShowEditorUtilities = fieldInfo;
                break;
            }

            bool showEditorUtilities = (bool)foundShowEditorUtilities.GetValue(target);

            showEditorUtilities = GUILayout.Toggle(showEditorUtilities, "Show Editor Utilities - Warning NON-SAFE");

            foundShowEditorUtilities.SetValue(target, showEditorUtilities);

            if (showEditorUtilities)
            {
                if (GUILayout.Button("Invoke Event"))
                {
                    if (target is IScriptableEventInvoker scriptableBaseEvent)
                        scriptableBaseEvent.Invoke();
                }
            }
        }
    }

    [UnityEditor.CustomEditor(typeof(BaseScriptableEvent<>), editorForChildClasses: true)]
    public class BaseScriptableEventDrawerGeneric : Sirenix.OdinInspector.Editor.OdinEditor
    {
        public override void OnInspectorGUI()
        {
            DrawUnityInspector();

            FieldInfo foundShowEditorUtilities = null;

            foreach (FieldInfo fieldInfo in target.GetType().GetFields(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (!fieldInfo.Name.Equals("showEditorUtilities"))
                    continue;

                foundShowEditorUtilities = fieldInfo;
                break;
            }

            bool showEditorUtilities = (bool)foundShowEditorUtilities.GetValue(target);

            showEditorUtilities = GUILayout.Toggle(showEditorUtilities, "Show Editor Utilities - Warning NON-SAFE");

            foundShowEditorUtilities.SetValue(target, showEditorUtilities);

             if (showEditorUtilities)
            {
                DrawEditorUtilities();
                DrawEditorData();
            }

            void DrawEditorData()
            {
                FieldInfo foundEditorData = null;

                foreach (FieldInfo fieldInfo in target.GetType().GetFields(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (!fieldInfo.Name.Equals("editorData"))
                        continue;

                    foundEditorData = fieldInfo;
                    break;
                }

                SerializedProperty serializedProperty = serializedObject.FindProperty(foundEditorData.Name);

                EditorGUILayout.PropertyField(serializedProperty, true);
                serializedObject.ApplyModifiedProperties();
            }

            void DrawEditorUtilities()
            {

                MethodInfo foundInvokeForEditorMethodInfo = null;

                foreach (MethodInfo methodInfo in target.GetType().GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (!methodInfo.Name.Equals("InvokeForEditor"))
                        continue;

                    foundInvokeForEditorMethodInfo = methodInfo;
                    break;
                }

                if (GUILayout.Button("Invoke Event"))
                {
                    foundInvokeForEditorMethodInfo.Invoke(target, null);
                }
            }
        }
    }

#endif
}




