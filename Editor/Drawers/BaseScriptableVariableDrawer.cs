using MSS.ScriptableEvents.Variables;
using System.Reflection;
using UnityEngine;

namespace MSS.ScriptableEvents.Editor
{
#if ODIN_INSPECTOR

    [UnityEditor.CustomEditor(typeof(GenericScriptableVariable<>), editorForChildClasses: true)]
    public class GenericScriptableVariableDrawer : Sirenix.OdinInspector.Editor.OdinEditor
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

                if (GUILayout.Button("Invoke On Value Changed"))
                {
                    foundInvokeForEditorMethodInfo.Invoke(target, null);
                }
            }
        }
    }

#elif !ODIN_INSPECTOR

    [UnityEditor.CustomEditor(typeof(GenericScriptableVariable<>), editorForChildClasses: true)]
    public class GenericScriptableVariableDrawer : UnityEditor.Editor
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

            showEditorUtilities = GUILayout.Toggle(showEditorUtilities, "Show Editor Utilities");

            foundShowEditorUtilities.SetValue(target, showEditorUtilities);

            if (showEditorUtilities)
            {
                DrawEditorUtilities();
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

                if (GUILayout.Button("Invoke On Value Changed"))
                {
                    foundInvokeForEditorMethodInfo.Invoke(target, null);
                }
            }
        }
    }
#endif

}
