namespace BoTech.UI.Forms.Rendering;

public class ComponentBuilderConfiguration
{
    /// <summary>
    /// The Component Type that should be instantiated by the Componentbuilder
    /// </summary>
    public Type ComponentType { get; init; }
    /// <summary>
    /// Properties that should be injected into the instance of the given ComponentType
    /// </summary>
    public List<ComponentBuilderAttributeConfiguration> ComponentAttributes { get; init; } = new();
    /// <summary>
    /// All visual children of this config.
    /// </summary>
    public List<ComponentBuilderConfiguration> Children { get; set; } = new();
}