using Avalonia;
using Avalonia.Controls;
using BoTech.UI.Forms.Avalonia.Rendering;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Rendering;
using BoTech.UI.Forms.Services;
using Material.Icons;
using Material.Icons.Avalonia;

namespace BoTech.UI.Forms.Avalonia.Controls;

public class HelpDescriptionOfFormElement : IHelpDescriptionOfFormElement
{
    
    public Guid Id { get; init; }
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    public string HelpText { get; init; }
    public string HelpLink { get; init; }
    public bool IsHelpTextMarkdown { get; init; }

    private Flyout _descriptionFlyout;
    private Guid _idOfDescriptionFlyout;
    
    private Button _descriptionButton;
    private Guid _idOfDescriptionButton;
    
    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        _idOfDescriptionFlyout = Guid.NewGuid();
        _idOfDescriptionButton = Guid.NewGuid();
        return new ComponentBuilderConfiguration(this)
        {
            Id = _idOfDescriptionButton,
            ComponentType = typeof(Button),
            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
            {
                ComponentBuilderAttributeConfiguration.CreateConstantAttributeWithControlAsValue("Flyout", new ComponentBuilderConfiguration(this)
                {
                    Id = _idOfDescriptionFlyout,
                    ComponentType  = typeof(Flyout),
                    ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                    {
                        ComponentBuilderAttributeConfiguration.CreateConstantAttributeWithControlAsValue("Content", new ComponentBuilderConfiguration(this)
                        {
                            ComponentType  = typeof(TextBlock),
                            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                            {
                                ComponentBuilderAttributeConfiguration.CreateConstantAttribute("Text", HelpText),
                            }
                        })
                    }
                }),
            },
            Children = new List<IComponentBuilderConfiguration>()
            {
                new ComponentBuilderConfiguration(this)
                {
                    ComponentType = typeof(MaterialIcon),
                    ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                    {
                        ComponentBuilderAttributeConfiguration.CreateConstantAttribute("Kind", MaterialIconKind.Help)
                    }
                }
            }
        };
    }
    
    public void TryToAddChild(IFormElement child)
    {
        throw new NotImplementedException();
    }
    
    public void OpenDescription()
    {
        if (_descriptionFlyout is null || _descriptionButton is null)
            LoadButtonAndFlyoutFromVisualTree();
        _descriptionFlyout!.ShowAt(_descriptionButton!);
    }

    public void CloseDescription()
    {
        if (_descriptionFlyout is null || _descriptionButton is null)
            LoadButtonAndFlyoutFromVisualTree();
        _descriptionFlyout!.Hide();
    }

    private void LoadButtonAndFlyoutFromVisualTree()
    {
        if(VisualSurfaceManager<AvaloniaObject>.Instance == null) 
            throw new InvalidOperationException("The instance of the Visual Surface Manager can not be null here. Init before calling OpenDescription");
        if(VisualSurfaceManager<AvaloniaObject>.Instance.CurrentVisualSurface == null)
            throw new InvalidOperationException("The instance of the VisualSurface can not be null here. Init before calling OpenDescription");
        AvaloniaObject flyoutWithoutCast = VisualSurfaceManager<AvaloniaObject>.Instance.CurrentVisualSurface.GetRenderedComponentFinder()
            .FindInVisualTreeById(_idOfDescriptionFlyout);
        if(flyoutWithoutCast is Flyout flyout)
            _descriptionFlyout = flyout;
        AvaloniaObject buttonWithoutCast = VisualSurfaceManager<AvaloniaObject>.Instance.CurrentVisualSurface.GetRenderedComponentFinder()
            .FindInVisualTreeById(_idOfDescriptionButton);
        if(buttonWithoutCast is Button button)
            _descriptionButton = button;
    }
}