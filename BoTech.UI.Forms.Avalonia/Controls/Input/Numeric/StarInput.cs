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
    public event EventHandler<ValueChangedEventArgs>? OnUserUpdatedValue;
    public IHelpDescriptionOfFormElement HelpDescriptionOfFormElement { get; private set; }
    public string Description { get; init; }
    public ushort Minimum { get; set; }
    public ushort Value { get => (ushort)InternalValue; }
    public ushort Maximum { get; set; }
    public ushort Increment { get; set; }
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }

    private float InternalValue { get; set; }

    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        (IComponentBuilderConfiguration mainConfig, IHelpDescriptionOfFormElement helpInfo) = new InputLayoutBuilder().BuildStandardLayoutConfiguration<ushort>(new ComponentBuilderConfiguration(this)
        {
            ComponentType = typeof(StarInputControl),
            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>
            {
                ComponentBuilderAttributeConfiguration.CreateBindingAttribute("MinimumProperty", Minimum,"Minimum", typeof(StarInput)),
                ComponentBuilderAttributeConfiguration.CreateBindingAttribute("MaximumProperty", Maximum, "Maximum", typeof(StarInput)),
                ComponentBuilderAttributeConfiguration.CreateBindingAttribute("CurrentValueProperty", InternalValue, nameof(InternalValue), typeof(StarInput)),
            }

        }, this);
        HelpDescriptionOfFormElement = helpInfo;
        return mainConfig;
    }

    public void TryToAddChild(IFormElement child)
    {
        throw new NotSupportedException();
    }

    
    public void OpenDescription()
    {
        throw new NotImplementedException();
    }

    public void CloseDescription()
    {
        throw new NotImplementedException();
    }

    public ushort GetNumberOfStarsThatAreChecked()
    {
        return Value;
    }

    public bool IsHalfOfLastStarChecked()
    {
        return InternalValue % 1 == 0.5f;
    }

    public ushort GetNumberOfStarsThatAreUnchecked()
    {
        if(Maximum >= Value) 
            return (ushort)(Maximum - Value);
        throw new InvalidOperationException("Value is higher than Maximum");
    }
}