using BoTech.UI.Forms.Rendering;
using BoTech.UI.Forms.Web.Html;

namespace BoTech.UI.Forms.Web.Rendering;

public class ComponentBuilderConfiguration : IComponentBuilderConfiguration
{
    /// <summary>
    /// You also can define an HTML element instead of a class based Component stored in <see cref="ComponentType"/>
    /// Only supported in the Web.
    /// </summary>
    public HtmlElements HtmlElementComponent { get; set; } = HtmlElements.None;
    /// <summary>
    /// The Component Type that should be instantiated by the Componentbuilder
    /// </summary>
    public Type? ComponentType { get; init; } = null;
    /// <summary>
    /// Properties that should be injected into the instance of the given ComponentType
    /// </summary>
    public List<ComponentBuilderAttributeConfiguration> ComponentAttributes { get; init; } = new();
    /// <summary>
    /// All visual children of this config.
    /// </summary>
    public List<IComponentBuilderConfiguration> Children { get; set; } = new();
}