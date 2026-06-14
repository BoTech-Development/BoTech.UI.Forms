using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Services;

public class VisualSurfaceManager<TControlTypeBase>  where TControlTypeBase : class
{
    public IVisualSurface<TControlTypeBase>? CurrentVisualSurface { get; init; }
    public static VisualSurfaceManager<TControlTypeBase>? Instance { get; private set; }

    public static void CreateInstance(IVisualSurface<TControlTypeBase> currentVisualSurface)
    {
        Instance = new VisualSurfaceManager<TControlTypeBase>(currentVisualSurface);
    }
    
    private VisualSurfaceManager(IVisualSurface<TControlTypeBase> currentVisualSurface)
    {
        CurrentVisualSurface = currentVisualSurface;
    }
}