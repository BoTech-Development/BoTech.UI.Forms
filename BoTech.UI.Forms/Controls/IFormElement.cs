using BoTech.UI.Forms.Controls.Layout;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Controls;

public interface IFormElement : IVisualControl
{
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    /// <summary>
    /// Returns a boolean indicating whether the object which implements <see cref="ILayoutElement"/> or <see cref="IContentElement"/> so that it can have children.
    /// </summary>
    /// <returns>true when the implementing object could contain at least one child.</returns>
    public bool CanAddChildrenToThis => this is IContentElement || this is ILayoutElement; 
    /// <summary>
    /// When the object which implements this interface is a <see cref="ILayoutElement"/> or <see cref="IContentElement"/>, this method can be used to add a child to it.
    /// </summary>
    /// <param name="child"></param>
    public void TryToAddChild(IFormElement child);
}