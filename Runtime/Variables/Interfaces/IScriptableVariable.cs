namespace MSS.ScriptableEvents
{
    public interface IScriptableVariable<T>
    {
        T Value { get; set; }
        BaseEvent<T> OnValueChanged { get; set; }
    }
}