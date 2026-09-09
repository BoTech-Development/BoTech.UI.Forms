using BoTech.UI.Forms.Models;

namespace BoTech.UI.Forms.Controls;

/// <summary>
/// This interface defined that the ForElement support displaying a title and subtitle and an icon.
/// </summary>
public interface ITitle
{
    /// <summary>
    /// The Title to show above the SubTitle
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// The subtitle to show under the Title
    /// </summary>
    public string SubTitle { get; set; }
    /// <summary>
    /// The icon to show anywhere.
    /// </summary>
    public Icon Icon { get; set; } 
}