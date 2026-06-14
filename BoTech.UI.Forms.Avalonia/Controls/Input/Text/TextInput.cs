using System;
using System.Collections.Generic;
using System.Text.Json;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using BoTech.UI.Forms.Avalonia.Rendering;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Input.Text;
using BoTech.UI.Forms.Rendering;

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
    public Guid Id { get; init; }
    public string Property { get; init; }
    public string Value { get; }
    public event EventHandler? OnUserUpdatedValue;
    public bool IsMultiline { get; init; }

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
                new ComponentBuilderConfiguration(this)
                {
                    ComponentType = typeof(TextBlock),
                    ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                    {
                        ComponentBuilderAttributeConfiguration.CreateConstantAttribute("Text", Name + ":"),
                        ComponentBuilderAttributeConfiguration.CreateConstantAttribute("FontSize", 18),
                        ComponentBuilderAttributeConfiguration.CreateConstantAttribute("FontWeight", FontWeight.SemiBold),
                        ComponentBuilderAttributeConfiguration.CreateConstantAttribute("VerticalAlignment", VerticalAlignment.Center),
                    }
                },
                new ComponentBuilderConfiguration(this)
                {
                    ComponentType = typeof(TextBox),
                    ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                    {
                        ComponentBuilderAttributeConfiguration.CreateBindingAttribute("TextProperty", Value, "Value",
                            typeof(TextInput)),
                        ComponentBuilderAttributeConfiguration.CreateConstantAttribute("AcceptsReturn", IsMultiline),
                        ComponentBuilderAttributeConfiguration.CreateConstantAttribute("IsReadOnly", IsEnabled),
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
    
    public void OpenDescription()
    {
        HelpDescriptionOfFormElement.OpenDescription();
    }

    public void CloseDescription()
    {
        HelpDescriptionOfFormElement.CloseDescription();
    }


}