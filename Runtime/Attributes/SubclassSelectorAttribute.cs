/*
    https://github.com/baba-s/Unity-SerializeReferenceExtensions/blob/master/Assets/SubclassSelectorAttribute/Scripts/Runtime/SubclassSelectorAttribute.cs
*/

#if UNITY_2019_3_OR_NEWER

using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class SubclassSelectorAttribute : PropertyAttribute
{
    bool m_includeMono;

    public SubclassSelectorAttribute(bool includeMono = false)
    {
        m_includeMono = includeMono;
    }

    public bool IsIncludeMono()
    {
        return m_includeMono;
    }
}

#endif