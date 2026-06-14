using Avalonia;
using Avalonia.Controls;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Avalonia.Rendering;

public class RenderedComponentFinder : IRenderedComponentFinder<AvaloniaObject>
{
    private readonly Dictionary<Guid, AvaloniaObject> _componentsByGuid = new();
    private readonly Dictionary<IFormElement, AvaloniaObject> _componentsByFormElement = new();
    public void Clear()
    {
        _componentsByGuid.Clear();
        _componentsByFormElement.Clear();
    }

    public void AddRenderedComponent(Guid id, IFormElement renderedFor, AvaloniaObject control)
    {
        _componentsByGuid.Add(id, control);
        if(!_componentsByFormElement.ContainsKey(renderedFor))
            _componentsByFormElement.Add(renderedFor, control);
    }

    public AvaloniaObject FindInVisualTreeById(Guid id)
    {
        if (_componentsByGuid.ContainsKey(id))
        {
            return _componentsByGuid[id];
        }
        throw new ArgumentException($"No rendered component found in the visual tree with id {id}");
    }

    public AvaloniaObject FindInVisualTreeByNameOfFormElement(string name)
    {
        throw new NotImplementedException();
    }

    public AvaloniaObject FindRootComponentInVisualTreeForFormElement(IFormElement formElement)
    {
        if (_componentsByFormElement.ContainsKey(formElement))
        {
            return _componentsByFormElement[formElement];
        }
        throw new ArgumentException($"No rendered component found in the visual tree with connected FormElement: {formElement}");
    }
    

}