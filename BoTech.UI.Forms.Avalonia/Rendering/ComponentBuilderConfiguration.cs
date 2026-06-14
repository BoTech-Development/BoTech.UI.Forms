using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Avalonia.Rendering;

public class ComponentBuilderConfiguration(IFormElement configurationFor) : IComponentBuilderConfiguration
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public IFormElement ConfigurationForFormElement { get; init; } = configurationFor;
    public Type ComponentType { get; init; }
    public List<ComponentBuilderAttributeConfiguration> ComponentAttributes { get; init; } = new List<ComponentBuilderAttributeConfiguration>();
    public List<IComponentBuilderConfiguration> Children { get; set; } = new List<IComponentBuilderConfiguration>();
}