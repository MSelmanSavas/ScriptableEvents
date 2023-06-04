using System.Collections;
using System.Collections.Generic;
using MSS.ScriptableEvents;
using UnityEngine;

public class ScriptableGenericEventListener<T> : BaseScriptableEventListener<T>
{
    [SerializeField]
    protected GenericEventListener<T> _eventListener = new();
    public override IEventListenerLogic<T> OnInvokedLogic => _eventListener;
    public override IEventListenerData<T> OnInvokedData => _eventListener;
    public override IEventListenerInvoker<T> OnInvokedActions => _eventListener;
}
