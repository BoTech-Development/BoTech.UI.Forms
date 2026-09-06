using BoTech.UI.Forms.Controls;

namespace BoTech.UI.Forms.Rendering;
/// <summary>
/// This class can be used to find a rendered Component by its id.
/// </summary>
/// <typeparam name="TControlTypeBase">The type of the Component base class => different for the web and avalonia package</typeparam>
public interface IRenderedComponentFinder<TControlTypeBase>  where TControlTypeBase : class
{
    public void Clear();
    public void AddRenderedComponent(Guid id, IFormElement renderedFor, TControlTypeBase control);
    public TControlTypeBase FindInVisualTreeById(Guid id);
    public TControlTypeBase FindInVisualTreeByNameOfFormElement(string name);
    public TControlTypeBase FindRootComponentInVisualTreeForFormElement(IFormElement formElement);
}