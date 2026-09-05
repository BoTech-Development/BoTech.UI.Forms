using Avalonia.Controls;
using BoTech.UI.Forms.Avalonia.Controls.Builder;
using BoTech.UI.Forms.Avalonia.Rendering;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Input;
using BoTech.UI.Forms.Controls.Input.DateTime;
using BoTech.UI.Forms.Controls.Layout;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Avalonia.Controls.Input.DateTime;

public class DateTimeInput : IDateTimeInput
{
    public Guid Id { get; init; } =  Guid.NewGuid();
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    public string Name { get; init; }
    private IHelpDescriptionOfFormElement HelpDescriptionOfFormElement { get;  set; }
    public string Description { get; init; }
    public string Property { get; init; }
    private System.DateTime _value =  System.DateTime.MinValue + new TimeSpan(365, 0, 0, 0);
    public System.DateTime Value
    {
        get => _value;
        private set
        {
            OnUserUpdatedValue?.Invoke(this, new ValueChangedEventArgs(_value, value));
            _value = value;
        }
    }

    public DateTimeOffset SelectedDate
    {
        get => new DateTimeOffset(Value);
        set
        {
            Value = new System.DateTime(DateOnly.FromDateTime(value.DateTime),TimeOnly.FromTimeSpan(SelectedTime));
        }
    }
    public TimeSpan SelectedTime
    {
        get => new TimeSpan(0, Value.Hour, Value.Minute, Value.Second,  Value.Millisecond, Value.Microsecond);
        set
        {
            Value = new System.DateTime(DateOnly.FromDateTime(SelectedDate.DateTime),TimeOnly.FromTimeSpan(value));
        }
    }

    public event EventHandler<ValueChangedEventArgs>? OnUserUpdatedValue;


    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        (IComponentBuilderConfiguration mainConfig, IHelpDescriptionOfFormElement helpInfo) = new InputLayoutBuilder().BuildStandardLayoutConfiguration<System.DateTime>(new List<IComponentBuilderConfiguration>
        {
            new ComponentBuilderConfiguration(this)
            {
                ComponentType = typeof(TimePicker),
                ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                {
                    ComponentBuilderAttributeConfiguration.CreateBindingAttribute("SelectedTimeProperty", SelectedTime, "SelectedTime", typeof(DateTimeInput)),
                    ComponentBuilderAttributeConfiguration.CreateConstantAttribute("ClockIdentifier", "24HourClock"),
                    ComponentBuilderAttributeConfiguration.CreateConstantAttribute("MinuteIncrement", 1)
                }
            },
            new ComponentBuilderConfiguration(this)
            {
                ComponentType = typeof(DatePicker),
                ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                {
                    ComponentBuilderAttributeConfiguration.CreateBindingAttribute("SelectedDateProperty", SelectedDate, "SelectedDate", typeof(DateTimeInput))
                }
            }
        }, this);
        HelpDescriptionOfFormElement = helpInfo;
        return mainConfig;
    }

    public void TryToAddChild(IFormElement child)
    {
        throw new NotSupportedException();
    }


    public void OpenDescription() => HelpDescriptionOfFormElement.OpenDescription();
    

    public void CloseDescription() => HelpDescriptionOfFormElement.CloseDescription();

    public void SetToCurrentDateTime()
    {
        Value = System.DateTime.Now;
        //SelectedTime = TimeOnly.FromDateTime(System.DateTime.Now).ToTimeSpan();
    }

}