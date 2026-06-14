using System;
using Avalonia;
using Avalonia.Controls;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Rendering;
using BoTech.UI.Forms.Services;

namespace BoTech.UI.Forms.Avalonia.Rendering;

public class VisualSurface : IVisualSurface<AvaloniaObject>
{
    private AvaloniaObject? _renderedRootElement = null;
    private IFormElement? _rootElement = null;
    private readonly RenderedComponentFinder _finder = new RenderedComponentFinder();

    public VisualSurface()
    {
        VisualSurfaceManager<AvaloniaObject>.CreateInstance(this);
    }
    public IRenderedComponentFinder<AvaloniaObject> GetRenderedComponentFinder()
    {
        return _finder;
    }

    public AvaloniaObject? GetPrerenderedRootElement()
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