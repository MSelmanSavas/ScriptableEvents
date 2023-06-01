using System.Reflection;
using UnityEditor;
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

                if (GUILayout.Button("Subscribe Persistent"))
                {
                    if (target is BaseScriptableEventListener scriptableBaseEvent)
                        scriptableBaseEvent.SubscribePersistent();
                }

                if (GUILayout.Button("UnSubscribe Persistent"))
                {
                    if (target is BaseScriptableEventListener scriptableBaseEvent)
                        scriptableBaseEvent.UnSubscribePersistent();
                }
            }
        }
    }

    [UnityEditor.CustomEditor(typeof(BaseScriptableEventListener<>), editorForChildClasses: true)]
    public class BaseScriptableEventListenerGenericDrawer : Sirenix.OdinInspector.Editor.OdinEditor
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

            showEditorUtilities = GUILayout.Toggle(showEditorUtilities, "Show Editor Utilities - Warning NON-SAFE");

            foundShowEditorUtilities.SetValue(target, showEditorUtilities);

            if (showEditorUtilities)
            {
                DrawEditorData();
                DrawOnInvokedMethod();
                DrawSubscribeMethod();
                DrawUnSubscribeMehtod();
                DrawSubscribePersistentMethod();
                DrawUnSubscribePersistentMehtod();
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

            void DrawOnInvokedMethod()
            {
                MethodInfo foundInvokeForEditorMethodInfo = null;

                foreach (MethodInfo methodInfo in serializedObject.targetObject.GetType().GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (!methodInfo.Name.Equals("InvokeForEditor"))
                        continue;

                    foundInvokeForEditorMethodInfo = methodInfo;
                    break;
                }

                if (GUILayout.Button("On Invoked"))
                {
                    foundInvokeForEditorMethodInfo.Invoke(serializedObject.targetObject, null);
                }
            }

            void DrawSubscribeMethod()
            {
                MethodInfo foundSubscribeMethodInfo = null;

                foreach (MethodInfo methodInfo in serializedObject.targetObject.GetType().GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (!methodInfo.Name.Equals("Subscribe"))
                        continue;

                    foundSubscribeMethodInfo = methodInfo;
                    break;
                }

                if (GUILayout.Button("Subscribe"))
                {
                    foundSubscribeMethodInfo.Invoke(serializedObject.targetObject, null);
                }
            }

            void DrawUnSubscribeMehtod()
            {
                MethodInfo foundUnSubscribeMethodInfo = null;

                foreach (MethodInfo methodInfo in serializedObject.targetObject.GetType().GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (!methodInfo.Name.Equals("UnSubscribe"))
                        continue;

                    foundUnSubscribeMethodInfo = methodInfo;
                    break;
                }

                if (GUILayout.Button("UnSubscribe"))
                {
                    foundUnSubscribeMethodInfo.Invoke(serializedObject.targetObject, null);
                }
            }

            void DrawSubscribePersistentMethod()
            {
                MethodInfo foundSubscribePersistenMethodInfo = null;

                foreach (MethodInfo methodInfo in serializedObject.targetObject.GetType().GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (!methodInfo.Name.Equals("SubscribePersistent"))
                        continue;

                    foundSubscribePersistenMethodInfo = methodInfo;
                    break;
                }

                if (GUILayout.Button("Subscribe Persistent"))
                {
                    foundSubscribePersistenMethodInfo.Invoke(serializedObject.targetObject, null);
                }
            }

            void DrawUnSubscribePersistentMehtod()
            {
                MethodInfo foundUnSubscribePersistentMethodInfo = null;

                foreach (MethodInfo methodInfo in serializedObject.targetObject.GetType().GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (!methodInfo.Name.Equals("UnSubscribePersistent"))
                        continue;

                    foundUnSubscribePersistentMethodInfo = methodInfo;
                    break;
                }

                if (GUILayout.Button("UnSubscribe Persistent"))
                {
                    foundUnSubscribePersistentMethodInfo.Invoke(serializedObject.targetObject, null);
                }
            }
        }
    }

#elif !ODIN_INSPECTOR

    [UnityEditor.CustomEditor(typeof(BaseScriptableEventListener), editorForChildClasses: true)]
    public class BaseScriptableEventListenerDrawer : Sirenix.OdinInspector.Editor.OdinEditor
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

                if (GUILayout.Button("Subscribe Persistent"))
                {
                    if (target is BaseScriptableEventListener scriptableBaseEvent)
                        scriptableBaseEvent.SubscribePersistent();
                }

                if (GUILayout.Button("UnSubscribe Persistent"))
                {
                    if (target is BaseScriptableEventListener scriptableBaseEvent)
                        scriptableBaseEvent.UnSubscribePersistent();
                }
            }
        }
    }

    [UnityEditor.CustomEditor(typeof(BaseScriptableEventListener<>), editorForChildClasses: true)]
    public class BaseScriptableEventListenerGenericDrawer : Sirenix.OdinInspector.Editor.OdinEditor
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
                DrawEditorData();
                DrawOnInvokedMethod();
                DrawSubscribeMethod();
                DrawUnSubscribeMehtod();
                DrawSubscribePersistentMethod();
                DrawUnSubscribePersistentMehtod();
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

            void DrawOnInvokedMethod()
            {
                MethodInfo foundInvokeForEditorMethodInfo = null;

                foreach (MethodInfo methodInfo in serializedObject.targetObject.GetType().GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (!methodInfo.Name.Equals("InvokeForEditor"))
                        continue;

                    foundInvokeForEditorMethodInfo = methodInfo;
                    break;
                }

                if (GUILayout.Button("On Invoked"))
                {
                    foundInvokeForEditorMethodInfo.Invoke(serializedObject.targetObject, null);
                }
            }

            void DrawSubscribeMethod()
            {
                MethodInfo foundSubscribeMethodInfo = null;

                foreach (MethodInfo methodInfo in serializedObject.targetObject.GetType().GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (!methodInfo.Name.Equals("Subscribe"))
                        continue;

                    foundSubscribeMethodInfo = methodInfo;
                    break;
                }

                if (GUILayout.Button("Subscribe"))
                {
                    foundSubscribeMethodInfo.Invoke(serializedObject.targetObject, null);
                }
            }

            void DrawUnSubscribeMehtod()
            {
                MethodInfo foundUnSubscribeMethodInfo = null;

                foreach (MethodInfo methodInfo in serializedObject.targetObject.GetType().GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (!methodInfo.Name.Equals("UnSubscribe"))
                        continue;

                    foundUnSubscribeMethodInfo = methodInfo;
                    break;
                }

                if (GUILayout.Button("UnSubscribe"))
                {
                    foundUnSubscribeMethodInfo.Invoke(serializedObject.targetObject, null);
                }
            }

            void DrawSubscribePersistentMethod()
            {
                MethodInfo foundSubscribePersistenMethodInfo = null;

                foreach (MethodInfo methodInfo in serializedObject.targetObject.GetType().GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (!methodInfo.Name.Equals("SubscribePersistent"))
                        continue;

                    foundSubscribePersistenMethodInfo = methodInfo;
                    break;
                }

                if (GUILayout.Button("Subscribe Persistent"))
                {
                    foundSubscribePersistenMethodInfo.Invoke(serializedObject.targetObject, null);
                }
            }

            void DrawUnSubscribePersistentMehtod()
            {
                MethodInfo foundUnSubscribePersistentMethodInfo = null;

                foreach (MethodInfo methodInfo in serializedObject.targetObject.GetType().GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (!methodInfo.Name.Equals("UnSubscribePersistent"))
                        continue;

                    foundUnSubscribePersistentMethodInfo = methodInfo;
                    break;
                }

                if (GUILayout.Button("UnSubscribe Persistent"))
                {
                    foundUnSubscribePersistentMethodInfo.Invoke(serializedObject.targetObject, null);
                }
            }
        }
    }


#endif
}


