using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using BoTech.UI.Forms.Avalonia.Rendering;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Input;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Avalonia.Controls.Input;

public class InputNameLeftOfInput : IInputNameLeftOfInput
{
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; }
    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        return new ComponentBuilderConfiguration(this)
        {
            ComponentType = typeof(TextBlock),
            ComponentAttributes = new List<IComponentBuilderAttributeConfiguration>()
            {
                ComponentAttributeFactory.CreateConstantAttribute("Text", Name + ":"),
                ComponentAttributeFactory.CreateConstantAttribute("FontSize", 18),
                ComponentAttributeFactory.CreateConstantAttribute("FontWeight", FontWeight.SemiBold),
                ComponentAttributeFactory.CreateConstantAttribute("VerticalAlignment",
                    VerticalAlignment.Center),
            }
        };
    }
    
    public void TryToAddChild(IFormElement child)
    {
        throw new NotSupportedException();
    }

   
}