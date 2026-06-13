using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Controls.Input;

public abstract class AInput : IInput<object>
{
    public string Description { get; init; }
    public void OpenDescription()
    {
        throw new NotImplementedException();
    }

    public void CloseDescription()
    {
        throw new NotImplementedException();
    }

    public string Name { get; init; }
    public Guid Id { get; init; }
    public string Property { get; init; }
    public object Value { get; set; }
    public event EventHandler? OnUserUpdatedValue;

    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }

    public void TryToAddChild(IFormElement child)
    {
        throw new NotSupportedException("You can not add a children to a AInput");
    }
    public void EvaluateType(){}
    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        throw new NotImplementedException();
    }
}