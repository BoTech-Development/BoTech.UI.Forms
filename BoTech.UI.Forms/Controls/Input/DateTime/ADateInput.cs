namespace BoTech.UI.Forms.Controls.Input.DateTime;

public class ADateInput : IInput<DateOnly>
{
    public string Description { get; init; }
    public string Name { get; init; }
    public string Property { get; init; }
    public DateOnly Value { get; set; }
    public void Show()
    {
        throw new NotImplementedException();
    }

   
    public void OnUserUpdatedValue()
    {
        throw new NotImplementedException();
    }
}