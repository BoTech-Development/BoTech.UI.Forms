using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Controls.Layout;

public interface IStack : ILayoutElement
{
    public List<IFormElement> Children { get; init; } 
    public Orientation Orientation { get; init; } 
}