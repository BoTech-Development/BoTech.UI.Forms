using System.ComponentModel;
using BoTech.UI.Forms.Rendering;
using Microsoft.AspNetCore.Components;

namespace BoTech.UI.Forms.Web.Demo.Components;

public partial class DynamicComponentHost : ComponentBase
{
   // private RenderTreeConfig _renderTreeConfig;
   //private bool _shouldRender = false;
    [Parameter] public RenderTreeConfig Config { get; set; } = new RenderTreeConfig(); //{ get => _renderTreeConfig; set => SetConfig(value); }
    
    public void AddAttribute(ComponentBuilderAttributeConfiguration attributeConfiguration)
    {
        if (attributeConfiguration.IsBindingProperty)
        {
            AddBindingProperty(attributeConfiguration);
        }
        else
        {
         //   Parameters[attributeConfiguration.AttributeName] = attributeConfiguration.AttributeValue;
        }
        
    }
    private void AddBindingProperty(ComponentBuilderAttributeConfiguration attributeConfiguration)
    {
        Console.WriteLine("AddBindingProperty");
        // Check if the property with the given name has an onChanged Event defined in the given componenttype
    }

  /*  private void SetConfig(RenderTreeConfig config)
    {
        _renderTreeConfig = config;
        _shouldRender = true;
        
    }
    protected override bool ShouldRender()
    {
        bool shouldRender = _shouldRender;
        _shouldRender = false;
        return shouldRender || base.ShouldRender();
    }*/

    public class RenderTreeConfig
    {
        public Type ComponentType { get; init; }
        public Dictionary<string, object> Parameters { get; init; } = new Dictionary<string, object>();
        public List<RenderTreeConfig> Children { get; set; } = new List<RenderTreeConfig>();
    }
}