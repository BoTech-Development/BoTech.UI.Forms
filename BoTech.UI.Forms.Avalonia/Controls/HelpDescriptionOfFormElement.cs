using System;
using System.Collections.Generic;
using Avalonia.Controls;
using BoTech.UI.Forms.Avalonia.Rendering;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Avalonia.Controls;

public class HelpDescriptionOfFormElement : IHelpDescriptionOfFormElement
{
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    public string HelpText { get; init; }
    public string HelpLink { get; init; }
    public bool IsHelpTextMarkdown { get; init; }
    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        return new ComponentBuilderConfiguration()
        {
            ComponentType = typeof(Button),
            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
            {
                ComponentBuilderAttributeConfiguration.CreateConstantAttributeWithControlAsValue("Flyout", new ComponentBuilderConfiguration()
                {
                    ComponentType  = typeof(Flyout),
                    ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                    {
                        ComponentBuilderAttributeConfiguration.CreateConstantAttributeWithControlAsValue("Content", new ComponentBuilderConfiguration()
                        {
                            ComponentType  = typeof(TextBlock),
                            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                            {
                                ComponentBuilderAttributeConfiguration.CreateConstantAttribute("Text", HelpText),
                            }
                        })
                    }
                }),
                
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