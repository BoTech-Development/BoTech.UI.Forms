using BoTech.UI.Forms.Controls;

namespace BoTech.UI.Forms.Rendering;
/// <summary>
/// This class builds the actual control for the specific UI-Framework.
/// </summary>
/// <typeparam name="TControlTypeBase">This generic Type declares the base control Type. For instance in blazor ControlType will be RenderFragment.</typeparam>
public interface IComponentBuilder<TControlTypeBase> where TControlTypeBase : class
{
    /// <summary>
    /// This method build the component to a specific base type which can be displayed.
    /// This method also adds the result (visualTree) to the implementation of <see cref="IRenderedComponentFinder{TControlTypeBase}"/>
    /// TODO: Remove side effect. => No clean code.
    /// </summary>
    /// <param name="instanceOfRootFormElement">The component build</param>
    /// <returns>a displayable implementation of the component and all child components</returns>
    public TControlTypeBase BuildComponent(IFormElement instanceOfRootFormElement);
}