using Avalonia.Controls;
using Avalonia.Layout;
using BoTech.UI.Forms.Avalonia.Controls.Builder;
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
    private IHelpDescriptionOfFormElement HelpDescriptionOfFormElement { get;  set; }
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
        (IComponentBuilderConfiguration mainConfig, IHelpDescriptionOfFormElement helpInfo) = new InputLayoutBuilder().BuildStandardLayoutConfiguration<DateOnly>(
            new ComponentBuilderConfiguration(this)
            {
                ComponentType = typeof(DatePicker),
                ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                {
                    ComponentBuilderAttributeConfiguration.CreateBindingAttribute("SelectedDateProperty", Value, "SelectedDateTime", typeof(DateInput))
                }
            },this);
        HelpDescriptionOfFormElement = helpInfo;
        return mainConfig;
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