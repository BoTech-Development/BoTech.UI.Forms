namespace BoTech.UI.Forms.Controls.Layout;

public interface ILayoutElement : IFormElement
{
    public List<IFormElement> Children { get; }
}