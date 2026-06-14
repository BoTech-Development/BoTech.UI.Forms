using BoTech.UI.Forms.Controls;

namespace BoTech.UI.Forms.Rendering;

public interface IVisualSurface<TControlTypeBase> where TControlTypeBase : class
{
    /// <summary>
    /// This method must return the unique instance of the component finder which can find components by their id.
    /// </summary>
    public IRenderedComponentFinder<TControlTypeBase> GetRenderedComponentFinder();
    /// <summary>
    /// Getter for the rendered root element, build by the <see cref="Render"/> method.
    /// </summary>
    /// <returns>the rendered item or null if <see cref="Render"/> method was not called or if <see cref="DeleteRootElement"/> was called.</returns>
    public TControlTypeBase? GetPrerenderedRootElement();
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