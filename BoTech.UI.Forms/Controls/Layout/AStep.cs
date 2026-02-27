namespace BoTech.UI.Forms.Controls.Layout;

public class AStep : IContentElement, ITitle
{
    public string Title { get; init; }
    public string SubTitle { get; init; }
    public IFormElement Content { get; set; }

    public void Show()
    {
        throw new NotImplementedException();
    }
}