using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Controls.Layout;

public interface IStep : IContentElement, ITitle
{
    public void NextPage();
    public void PreviousPage();
}