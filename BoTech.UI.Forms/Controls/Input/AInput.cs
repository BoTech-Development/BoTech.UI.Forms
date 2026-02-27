namespace BoTech.UI.Forms.Controls.Input;

public class AInput : IInput<object>
{
    public string Description { get; init; }
    public string Name { get; init; }
    public string Property { get; init; }
    public object Value { get; set; }
    public void Show()
    {
        throw new NotImplementedException();
    }

    public void EvaluateType(){}
    public void OnUserUpdatedValue()
    {
        throw new NotImplementedException();
    }
}