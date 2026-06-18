using Avalonia.Controls;
using Avalonia.Layout;
using BoTech.UI.Forms.Avalonia.Rendering;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Input;
using BoTech.UI.Forms.Controls.Input.DateTime;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Avalonia.Controls.Input.DateTime;

public class DateInput : IDateInput
{
        public Guid Id { get; init; } =  Guid.NewGuid();
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    public string Name { get; init; }
    public IHelpDescriptionOfFormElement HelpDescriptionOfFormElement { get; private set; }
    public string Description { get; init; }
    public string Property { get; init; }
    public DateOnly Value
    {
        get => field;
        private set
        {
            OnUserUpdatedValue?.Invoke(this,new ValueChangedEventArgs(field, value));
            field = value;
        }
    }
    public DateTimeOffset SelectedDateTime
    {
        get => field;
        set
        {
            field = value;
            Value = DateOnly.FromDateTime(field.DateTime);
        }
    }

    public event EventHandler<ValueChangedEventArgs>? OnUserUpdatedValue;
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
                    ComponentType = typeof(DatePicker),
                    ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                    {
                        ComponentBuilderAttributeConfiguration.CreateBindingAttribute("SelectedDateProperty", Value, "SelectedDateTime",
                            typeof(DateInput)),
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
    

    public void CloseDescription() => HelpDescriptionOfFormElement.CloseDescription();


    public void SetToCurrentDate()
    {
        SelectedDateTime = DateTimeOffset.Now;
    }
}