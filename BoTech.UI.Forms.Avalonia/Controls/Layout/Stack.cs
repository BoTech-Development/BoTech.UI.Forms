using Avalonia.Controls;
using Avalonia.Layout;
using BoTech.UI.Forms.Avalonia.Rendering;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Avalonia.Controls.Layout;

public class Stack : BoTech.UI.Forms.Controls.Layout.IStack
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    public List<IFormElement> Children { get; init; } = new();
    public BoTech.UI.Forms.Controls.Layout.Orientation Orientation { get; init; }
    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        var orientationCasted = Enum.Parse(typeof(Orientation), Orientation.ToString());
        return new ComponentBuilderConfiguration(this)
        {
            ComponentType = typeof(StackPanel),
            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
            {
                ComponentBuilderAttributeConfiguration.CreateConstantAttribute("Orientation", orientationCasted),
            }
        };
    }

    public void TryToAddChild(IFormElement child)
    {
        Children.Add(child);
    }
}