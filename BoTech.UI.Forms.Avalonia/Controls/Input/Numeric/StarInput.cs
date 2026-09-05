using Avalonia.Controls;
using BoTech.UI.Forms.Avalonia.Controls.Builder;
using BoTech.UI.Forms.Avalonia.CustomAvaloniaControls.StarInput;
using BoTech.UI.Forms.Avalonia.Rendering;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Input;
using BoTech.UI.Forms.Controls.Input.Numeric;
using BoTech.UI.Forms.Controls.Layout;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Avalonia.Controls.Input.Numeric;

public class StarInput : IStarInput
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; }
    public string Property { get; init; }
    public byte Value { get; }
    public event EventHandler<ValueChangedEventArgs>? OnUserUpdatedValue;
    public byte Maximum { get; set; }
    public byte NumberOfStarsThatAreChecked { get; init; }
    public bool IsHalfOfLastStarChecked { get; init; }
    public byte NumberOfStarsThatAreUnchecked { get; init; }
    public IHelpDescriptionOfFormElement HelpDescriptionOfFormElement { get; private set; }
    
    public string Description { get; init; }
    public byte Minimum { get; set; }
    public byte Increment { get; set; }
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        (IComponentBuilderConfiguration mainConfig, IHelpDescriptionOfFormElement helpInfo) = new InputLayoutBuilder().BuildStandardLayoutConfiguration<byte>(new ComponentBuilderConfiguration(this)
        {
            ComponentType = typeof(StarInputControl),
            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>
            {
                ComponentBuilderAttributeConfiguration.CreateBindingAttribute("CountOfStarsToDisplayProperty", 5, nameof(Maximum), typeof(StarInput)),
                ComponentBuilderAttributeConfiguration.CreateBindingAttribute("Minimum", Minimum, nameof(Minimum), typeof(StarInput)),
                ComponentBuilderAttributeConfiguration.CreateBindingAttribute("Increment", Increment, nameof(Increment), typeof(StarInput)),
                ComponentBuilderAttributeConfiguration.CreateBindingAttribute("Value", Value, nameof(Value), typeof(StarInput)),
                ComponentBuilderAttributeConfiguration.CreateBindingAttribute("IsVisible", IsVisible, nameof(IsVisible), typeof(StarInput)),
                ComponentBuilderAttributeConfiguration.CreateBindingAttribute("IsEnabled", IsEnabled, nameof(IsEnabled), typeof(StarInput))
            }

        }, this);
        HelpDescriptionOfFormElement = helpInfo;
        return mainConfig;
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