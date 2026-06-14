namespace BoTech.UI.Forms.Controls.Input.Text;

public interface ISearchTextInput : ITextInput
{
    public string StaticItemSource { get; set; }
    public IEnumerable<string> ItemSource { get; set; }
    public string SortByRegex { get; set; }
}