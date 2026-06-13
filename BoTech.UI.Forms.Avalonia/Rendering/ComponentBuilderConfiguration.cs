using System;
using System.Collections.Generic;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Avalonia.Rendering;

public class ComponentBuilderConfiguration : IComponentBuilderConfiguration
{
    public Type ComponentType { get; init; }

    public List<ComponentBuilderAttributeConfiguration> ComponentAttributes { get; init; } =
        new List<ComponentBuilderAttributeConfiguration>();
    public List<IComponentBuilderConfiguration> Children { get; set; } = new List<IComponentBuilderConfiguration>();
}