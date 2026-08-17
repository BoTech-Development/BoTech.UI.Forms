using Avalonia.Controls;
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
    public int NumberOfStarsThatAreChecked { get; init; }
    public bool IsHalfOfLastStarChecked { get; init; }
    public int NumberOfStarsThatAreUnchecked { get; init; }
    public IHelpDescriptionOfFormElement HelpDescriptionOfFormElement { get; private set; }
    
    public string Description { get; init; }
    public byte Minimum { get; set; }
    public byte Increment { get; set; }
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        HelpDescriptionOfFormElement = new HelpDescriptionOfFormElement()
        {
            IsEnabled = this.IsEnabled,
            IsVisible = this.IsVisible,
            HelpText = this.Description,
        };
        return new ComponentBuilderConfiguration(this)
        {
            ComponentType =
                typeof(StackPanel),
            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
            {
                ComponentBuilderAttributeConfiguration.CreateConstantAttribute("Orientation", Orientation.Horizontal),
            },
            Children = new List<IComponentBuilderConfiguration>()
            {
                new InputNameLeftOfInput()
                {
                    Name = this.Name,
                }.BuildComponentBuilderConfigurationFromThis(),
                new ComponentBuilderConfiguration(this)
                {
                    ComponentType = typeof(NumericUpDown),

                },
                HelpDescriptionOfFormElement.BuildComponentBuilderConfigurationFromThis()
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