using BoTech.UI.Forms.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BoTech.UI.Forms.Web.Rendering;

public class ComponentBuilder : IComponentBuilder<RenderFragment>
{
    public RenderFragment BuildComponentFromConfig(ComponentBuilderConfiguration config)
    {
        return BuildComponentFromConfig(config, 0);
    }

    private RenderFragment BuildComponentFromConfig(ComponentBuilderConfiguration config, int sequenceCounter)
    {
        return builder =>
        {
            Console.WriteLine("Staring to build component from config...");
            BuildComponentFromConfigRecursive(config, builder, sequenceCounter);
        };
    }

    private void BuildComponentFromConfigRecursive(ComponentBuilderConfiguration config, RenderTreeBuilder builder, int sequenceCounter)
    {
        sequenceCounter += 1;
        builder.OpenComponent(sequenceCounter, config.ComponentType);
        Console.WriteLine($"Current Component type to build: {config.ComponentType}");
        foreach (ComponentBuilderAttributeConfiguration attributeConfiguration in config.ComponentAttributes)
        {
            Console.WriteLine($"└─>Adding Attribute {attributeConfiguration.AttributeName}");
            builder.AddAttribute(sequenceCounter, attributeConfiguration.AttributeName, attributeConfiguration.AttributeValue);
        }

        if (config.Children.Count > 0)
        {
            builder.AddAttribute(sequenceCounter + 1, "ChildContent", (RenderFragment)(childBuilder =>
            {
                int childSequenceCounter = sequenceCounter + 1;
                foreach (ComponentBuilderConfiguration configChild in config.Children)
                {
                    childBuilder.AddContent(childSequenceCounter, BuildComponentFromConfig(configChild, childSequenceCounter));
                }
            }));
        }
        builder.CloseComponent();
    }
}