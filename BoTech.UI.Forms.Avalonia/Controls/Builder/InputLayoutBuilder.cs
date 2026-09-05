using Avalonia.Controls;
using BoTech.UI.Forms.Avalonia.Controls.Input;
using BoTech.UI.Forms.Avalonia.CustomAvaloniaControls.StarInput;
using BoTech.UI.Forms.Avalonia.Rendering;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Builder;
using BoTech.UI.Forms.Controls.Input;
using BoTech.UI.Forms.Controls.Layout;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Avalonia.Controls.Builder;

public class InputLayoutBuilder : IInputLayoutBuilder
{
    public (IComponentBuilderConfiguration, IHelpDescriptionOfFormElement) BuildStandardLayoutConfiguration<T>(List<IComponentBuilderConfiguration> innerComponents, IInput<T> instanceOfControl)
    {
        HelpDescriptionOfFormElement helpDescription = new HelpDescriptionOfFormElement()
        {
            IsEnabled = instanceOfControl.IsEnabled,
            IsVisible = instanceOfControl.IsVisible,
            HelpText = instanceOfControl.Description,
        };
        ComponentBuilderConfiguration mainConfig = new ComponentBuilderConfiguration(instanceOfControl)
        {
            ComponentType = typeof(StackPanel),
            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
            {
                ComponentBuilderAttributeConfiguration.CreateConstantAttribute("Orientation", Orientation.Horizontal),
            }
        };
        mainConfig.Children.Add(new InputNameLeftOfInput()
        {
            Name = instanceOfControl.Name,
        }.BuildComponentBuilderConfigurationFromThis());
        mainConfig.Children.AddRange(innerComponents);
        mainConfig.Children.Add(helpDescription.BuildComponentBuilderConfigurationFromThis());
        return (mainConfig, helpDescription);
    }
    public (IComponentBuilderConfiguration, IHelpDescriptionOfFormElement) BuildStandardLayoutConfiguration<T>(IComponentBuilderConfiguration innerComponent, IInput<T> instanceOfControl)
    {
        return BuildStandardLayoutConfiguration<T>(new List<IComponentBuilderConfiguration>() { innerComponent }, instanceOfControl);
    }
}