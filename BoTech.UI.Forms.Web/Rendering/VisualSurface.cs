using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Rendering;
using Microsoft.AspNetCore.Components;

namespace BoTech.UI.Forms.Web.Rendering;

public class VisualSurface : IVisualSurface<RenderFragment>
{

    private RenderFragment? _prerenderedContent;
    
    private IComponentBuilder<RenderFragment> _builder;
    
    private IFormElement? _content = null;

    public VisualSurface()
    {
        _builder = new ComponentBuilder();
    }

    public RenderFragment? GetPrerenderedRootElement()
    {
        return _prerenderedContent;
    }

    public void Render()
    {
        if (_content == null) throw new InvalidOperationException("Add content before rendering.");
        _prerenderedContent = _builder.BuildComponent( _content);
    }

    private void RenderRecursive(IFormElement element)
    {
        //Content.
    }
    public void UpdateRootElement(IFormElement element)
    {
        _content = element;
    }
    public void DeleteRootElement()
    {
        _content = null;
    }
    
}
