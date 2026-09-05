using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using BoTech.UI.Forms.Avalonia.Controls.Builder;
using BoTech.UI.Forms.Avalonia.Controls.Input.Numeric;
using BoTech.UI.Forms.Avalonia.CustomAvaloniaControls.StarInput;
using BoTech.UI.Forms.Avalonia.Rendering;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Input;
using BoTech.UI.Forms.Controls.Input.Text;
using BoTech.UI.Forms.Rendering;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace BoTech.UI.Forms.Avalonia.Controls.Input.Text;

public class TextInput : ITextInput
{
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    public string Name { get; init; }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string Description { get; init; }
    public IHelpDescriptionOfFormElement HelpDescriptionOfFormElement { get; private set; }
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Property { get; init; }
    public string Value
    {
        get => field;
        private set
        {
            OnUserUpdatedValue?.Invoke(this,new ValueChangedEventArgs(field, value));
            field = value;
        }
    }

    public event EventHandler<ValueChangedEventArgs>? OnUserUpdatedValue;
    public bool IsMultiline { get; init; }

    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        (IComponentBuilderConfiguration mainConfig, IHelpDescriptionOfFormElement helpInfo) = new InputLayoutBuilder().BuildStandardLayoutConfiguration<string>(new ComponentBuilderConfiguration(this)
        {
            ComponentType = typeof(TextBox),
            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
            {
                ComponentBuilderAttributeConfiguration.CreateBindingAttribute("TextProperty", Value, "Value", typeof(TextInput)),
                ComponentBuilderAttributeConfiguration.CreateConstantAttribute("AcceptsReturn", IsMultiline),
                ComponentBuilderAttributeConfiguration.CreateConstantAttribute("IsReadOnly", IsEnabled),
                ComponentBuilderAttributeConfiguration.CreateConstantAttribute("VerticalAlignment", VerticalAlignment.Center)
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
        HelpDescriptionOfFormElement.OpenDescription();
    }

    public void CloseDescription()
    {
        HelpDescriptionOfFormElement.CloseDescription();
    }


}