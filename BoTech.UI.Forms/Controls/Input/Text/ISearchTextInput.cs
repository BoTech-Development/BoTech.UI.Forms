namespace BoTech.UI.Forms.Controls.Input.Text;

public interface ISearchTextInput : ITextInput
{
    public string StaticItemSource { get; set; }
    public string ItemSource { get; set; }
    public string SortBy { get; set; }
}