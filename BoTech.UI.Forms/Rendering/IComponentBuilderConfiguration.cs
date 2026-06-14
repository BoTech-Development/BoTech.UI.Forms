using System.Text.Json.Serialization;
using BoTech.UI.Forms.Controls;

namespace BoTech.UI.Forms.Rendering;

public interface IComponentBuilderConfiguration
{
    /// <summary>
    /// Each component should have its own id, which make it findable with the <see cref="IRenderedComponentFinder{TControlTypeBase}"/> interface.
    /// </summary>
    public Guid Id { get; init; }
    /// <summary>
    /// Value for serializing <see cref="ComponentType"/>
    /// </summary>
   // public string FullComponentTypeName { get; init; }
    public IFormElement ConfigurationForFormElement { get; init; }
    /// <summary>
    /// The Component Type that should be instantiated by the Componentbuilder
    /// </summary>
   //[JsonIgnore]
    public Type ComponentType { get; init; }
    /// <summary>
    /// Properties that should be injected into the instance of the given ComponentType
    /// </summary>
    public List<ComponentBuilderAttributeConfiguration> ComponentAttributes { get; init; }
    /// <summary>
    /// All visual children of this config.
    /// </summary>
    public List<IComponentBuilderConfiguration> Children { get; set; }
}