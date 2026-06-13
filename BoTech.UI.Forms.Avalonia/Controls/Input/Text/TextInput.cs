using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Layout;
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
    public Guid Id { get; init; }
    public string Property { get; init; }
    public string Value { get; }
    public event EventHandler? OnUserUpdatedValue;
    public bool IsMultiline { get; init; }
    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        return new ComponentBuilderConfiguration()
        {
            ComponentType = typeof(StackPanel),
            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
            {
                ComponentBuilderAttributeConfiguration.CreateConstantAttribute("Orientation", Orientation.Horizontal),
            },
            Children = new List<IComponentBuilderConfiguration>()
            {
                new ComponentBuilderConfiguration()
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
                new HelpDescriptionOfFormElement()
                {
                    IsEnabled = this.IsEnabled,
                    IsVisible = this.IsVisible,
                    HelpText = this.Description,
                }.BuildComponentBuilderConfigurationFromThis()
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