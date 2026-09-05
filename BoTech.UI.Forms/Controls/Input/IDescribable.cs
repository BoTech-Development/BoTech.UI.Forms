namespace BoTech.UI.Forms.Controls.Input;

public interface IDescribable
{
    /// <summary>
    /// This IFormElement displays the flyout which contains the help info.
    /// </summary>
    //public IHelpDescriptionOfFormElement HelpDescriptionOfFormElement { get; }
    public string Description { get; init; }
    /// <summary>
    /// Opens the Description flyout
    /// </summary>
    public void OpenDescription();
    /// <summary>
    /// Closes the description flyout
    /// </summary>
    public void CloseDescription();
}