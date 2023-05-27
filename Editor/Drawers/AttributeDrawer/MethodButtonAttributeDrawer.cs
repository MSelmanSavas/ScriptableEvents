using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MethodButtonAttribute), useForChildren: true)]
public class MethodButtonAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Debug.LogError("Here ass");
        base.OnGUI(position, property, label);

        Debug.LogError("Here ass1");
        if (attribute is not MethodButtonAttribute methodButtonAttribute)
            return;


        Debug.LogError("Here ass2");
        if (GUILayout.Button(methodButtonAttribute.MethodName))
        {
            Debug.LogError("Here ass3");
            Debug.LogError(property.objectReferenceValue);
        }

        string methodName = methodButtonAttribute.MethodName;
        Object target = property.serializedObject.targetObject;
        System.Type type = target.GetType();

        System.Reflection.MethodInfo method = type.GetMethod(methodName);

        if (method == null)
        {
            GUI.Label(position, "Method could not be found. Is it public?");
            return;
        }

        if (method.GetParameters().Length > 0)
        {
            GUI.Label(position, "Method cannot have parameters.");
            return;
        }

        if (GUI.Button(position, method.Name))
        {
            method.Invoke(target, null);
        }
    }
}
