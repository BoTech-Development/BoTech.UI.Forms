using System;
using Avalonia.Controls;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Avalonia.Rendering;

public class VisualSurface : IVisualSurface<Control>
{
    private Control? _renderedRootElement = null;
    private IFormElement? _rootElement = null;
    public Control? GetPrerenderedRootElement()
    {
        return _renderedRootElement;
    }

    public void Render()
    {
        if (_rootElement != null)
            _renderedRootElement = new ComponentBuilder().BuildComponent(_rootElement);
        else
            throw new InvalidOperationException("Cannot render a null root element.");
    }

    public void UpdateRootElement(IFormElement element)
    {
        _rootElement = element;
    }

    public void DeleteRootElement()
    {
        _renderedRootElement = null;
        _rootElement = null;
    }
}