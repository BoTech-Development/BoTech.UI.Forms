using System.Numerics;
using Avalonia.Controls;
using BoTech.UI.Forms.Avalonia.Controls.Input.DateTime;
using BoTech.UI.Forms.Avalonia.Rendering;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Input;
using BoTech.UI.Forms.Controls.Input.Numeric;
using BoTech.UI.Forms.Controls.Layout;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Avalonia.Controls.Input.Numeric;

public class NumberInput<T> : INumberInput<T> where T : INumber<T>
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    
    public string Name { get; init; }
    public string Property { get; init; }
    public IHelpDescriptionOfFormElement HelpDescriptionOfFormElement { get; private set;  }
    public string Description { get; init; } = "";

    public T Value
    {
        get => field;
        private set
        {
            if (value > Maximum)
            {
                OnUserUpdatedValue?.Invoke(this, new ValueChangedEventArgs(field, Maximum));
                field = Maximum;
            }
            else if (value < Minimum)
            {
                OnUserUpdatedValue?.Invoke(this, new ValueChangedEventArgs(field, Minimum));
                field = Minimum;
            }
            else
            {
                OnUserUpdatedValue?.Invoke(this, new ValueChangedEventArgs(field, value));
                field = value;
            }
        }
    } = default(T);

    public event EventHandler<ValueChangedEventArgs>? OnUserUpdatedValue;
    public T Maximum { get; set; } = default(T);
    public T Minimum { get; set; } = default(T);
    public T Increment { get; set; } = default(T);
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
            ComponentType = typeof(StackPanel),
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
                    ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                    {
                        ComponentBuilderAttributeConfiguration.CreateBindingAttribute("ValueProperty", Value, "Value",
                            typeof(NumberInput<T>)),
                        ComponentBuilderAttributeConfiguration.CreateBindingAttribute("IncrementProperty", Increment, "Increment",
                            typeof(NumberInput<T>)),
                        ComponentBuilderAttributeConfiguration.CreateBindingAttribute("MinimumProperty", Minimum, "Minimum",
                            typeof(NumberInput<T>)),
                        ComponentBuilderAttributeConfiguration.CreateBindingAttribute("MaximumProperty", Maximum, "Maximum",
                        typeof(NumberInput<T>)),

                    }
                },
                HelpDescriptionOfFormElement.BuildComponentBuilderConfigurationFromThis()
            }
        };
    }

    public void TryToAddChild(IFormElement child)
    {
        throw new NotSupportedException();
    }
    
    public void OpenDescription() => HelpDescriptionOfFormElement.OpenDescription();

    public void CloseDescription() =>  HelpDescriptionOfFormElement.CloseDescription();
}