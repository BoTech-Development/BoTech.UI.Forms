namespace BoTech.UI.Forms.Rendering;

public class ComponentBuilderConfiguration
{
    public Type ComponentType { get; init; }
    public List<ComponentBuilderAttributeConfiguration> ComponentAttributes { get; init; } = new();
}