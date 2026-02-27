namespace BoTech.UI.Forms.Controls.Input.Text;

public class ATextInput : IInput<string>
{
    public string Description { get; init; }
    public string Name { get; init; }
    public string Property { get; init; }
    public string Value { get; private set; }
    public void Show()
    {
        throw new NotImplementedException();
    }
    public void OnUserUpdatedValue()
    {
        throw new NotImplementedException();
    }
}