namespace BoTech.UI.Forms.Controls.Input;

public interface IDescribable
{
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