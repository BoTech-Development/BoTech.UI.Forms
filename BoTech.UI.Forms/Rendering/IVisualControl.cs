using BoTech.UI.Forms.Controls;

namespace BoTech.UI.Forms.Rendering;

public interface IVisualControl : IUniqueElement
{
    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis();
}