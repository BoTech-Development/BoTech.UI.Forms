using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Input.Text;
using BoTech.UI.Forms.Controls.Layout;
using BoTech.UI.Forms.Rendering;
using MudBlazor;

namespace BoTech.UI.Forms.Web.Controls.Input.Text;

public class TextInput : ITextInput
{
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    public string Name { get; init; }
    public Guid Id { get; init; }
    public string Description { get; init; }
    public string Property { get; init; }
    public string Value { get; }
    public event EventHandler? OnUserUpdatedValue;
    
    public ComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        return new ComponentBuilderConfiguration()
        {
            ComponentType = typeof(MudTextField<string>),
            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
            {
                new ComponentBuilderAttributeConfiguration()
                {
                    AttributeName = "HelperText",
                    AttributeValue = "Some Name"
                }
            }
        };
    }
    
    public void TryToAddChild(IFormElement child)
    {
        throw new NotImplementedException();
    }

    public void OpenDescription()
    {
        throw new NotImplementedException();
    }

    public void CloseDescription()
    {
        throw new NotImplementedException();
    }
}