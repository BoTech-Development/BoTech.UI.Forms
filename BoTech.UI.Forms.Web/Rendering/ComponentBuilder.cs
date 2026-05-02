using BoTech.UI.Forms.Rendering;
using Microsoft.AspNetCore.Components;

namespace BoTech.UI.Forms.Web.Rendering;

public class ComponentBuilder : IComponentBuilder<RenderFragment>
{
    private int _sequenceCounter = 0;
    public RenderFragment BuildComponentFromConfig(ComponentBuilderConfiguration config)
    {
        _sequenceCounter += 1;
        return builder =>
        {
            builder.OpenComponent(_sequenceCounter, config.ComponentType);
            foreach (ComponentBuilderAttributeConfiguration attributeConfiguration in config.ComponentAttributes)
            {
                builder.AddAttribute(_sequenceCounter, attributeConfiguration.AttributeName, attributeConfiguration.AttributeValue);
            }
            builder.CloseComponent();
        };
        
    }
}