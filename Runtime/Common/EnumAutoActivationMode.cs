namespace MSS.ScriptableEvents
{
    [System.Flags]
    public enum EnumAutoActivationMode
    {
        None,
        PlayMode,
        [UnityEngine.InspectorName("EditorMode - CAUTION")]
        EditorMode,
    }
}
