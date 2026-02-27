namespace BoTech.UI.Forms.Controls;

public interface IContentElement : IFormElement
{
    public IFormElement Content { get; set; }
}