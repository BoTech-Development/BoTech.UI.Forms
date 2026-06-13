namespace BoTech.UI.Forms.Controls;
/// <summary>
/// This Interface can be used to implement a help Button, which opens an info dialog.
/// </summary>
public interface IHelpDescriptionOfFormElement : IFormElement
{
    /// <summary>
    /// The text that should be displayed in the dialog
    /// </summary>
    public string HelpText { get; init; }
    /// <summary>
    /// An external Link which could link to a more detailed help page.
    /// </summary>
    public string HelpLink { get; init; }
    /// <summary>
    /// When true the helpText will be rendered as markdown, else the text will be displayed has plain text.
    /// </summary>
    public bool IsHelpTextMarkdown { get; init; }
    /// <summary>
    /// Opens the description dialog
    /// </summary>
    public void OpenDescription();
    /// <summary>
    /// Closes the description dialog.
    /// </summary>
    public void CloseDescription();
}