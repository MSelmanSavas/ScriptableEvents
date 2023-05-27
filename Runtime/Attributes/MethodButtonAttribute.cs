using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class MethodButtonAttribute : PropertyAttribute
{
    public string MethodName;

    public MethodButtonAttribute()
    {
        MethodName = "";
    }

    public MethodButtonAttribute(string methodName)
    {
        MethodName = methodName;
    }
}
