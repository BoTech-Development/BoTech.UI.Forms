using BoTech.UI.Forms.Controls;

namespace BoTech.UI.Forms.Rendering;

public interface IVisualSurface
{
    /// <summary>
    /// Render the visual surface in the UI window / webpage
    /// </summary>
    public void Render();
    /// <summary>
    /// Adds a new Element to the root of the visual surface
    /// </summary>
    /// <param name="element">The new Element</param>
    public void UpdateRootElement(IFormElement element);
    /// <summary>
    /// Removes the current root Element from the visual surface
    /// No rendering.
    /// </summary>
    public void DeleteRootElement();
}