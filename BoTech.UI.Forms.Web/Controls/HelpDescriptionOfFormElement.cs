using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Rendering;
using BoTech.UI.Forms.Web.Html;
using BoTech.UI.Forms.Web.Rendering;
using MudBlazor;

namespace BoTech.UI.Forms.Web.Controls;

public class HelpDescriptionOfFormElement : IHelpDescriptionOfFormElement
{
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    public string HelpText { get; init; }
    public string HelpLink { get; init; }
    public bool IsHelpTextMarkdown { get; init; }

    public HelpDescriptionOfFormElement()
    {
        IsHelpTextMarkdown = false;
        HelpText = "";
        HelpLink = "";
        IsVisible = true;
        IsEnabled = true;
    }
    private bool _isOpened = false;
    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        return new ComponentBuilderConfiguration()
        {
            HtmlElementComponent = HtmlElements.Div,
            Children = new List<IComponentBuilderConfiguration>()
            {
                new ComponentBuilderConfiguration()
                {
                    ComponentType = typeof (MudToggleIconButton),
                    ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                    {
                        ComponentBuilderAttributeConfiguration.CreateConstantAttribute("Icon", Icons.Material.Filled.Info),
                        ComponentBuilderAttributeConfiguration.CreateConstantAttribute("ToggledIcon", Icons.Material.Filled.Close),
                        ComponentBuilderAttributeConfiguration.CreateBindingAttribute("Toggled", false, nameof(_isOpened), this.GetType())
                    }
                },
                new ComponentBuilderConfiguration()
                {
                    ComponentType = typeof (MudPopover),
                    ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                    {
                        ComponentBuilderAttributeConfiguration.CreateBindingAttribute("Open", false, nameof(_isOpened), this.GetType())
                    },
                    Children = new List<IComponentBuilderConfiguration>()
                    {
                        new ComponentBuilderConfiguration()
                        {
                            ComponentType = typeof (MudStack),
                            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                            {
                                ComponentBuilderAttributeConfiguration.CreateConstantAttribute("Row", true)
                            },
                            Children = new List<IComponentBuilderConfiguration>()
                            {
                                new ComponentBuilderConfiguration()
                                {
                                    ComponentType = typeof (MudAlert),
                                    ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                                    {
                                        ComponentBuilderAttributeConfiguration.CreateConstantAttribute("Severity", Severity.Info),
                                    },
                                    Children = new List<IComponentBuilderConfiguration>()
                                    {
                                        new ComponentBuilderConfiguration()
                                        {
                                            ComponentType = typeof (MudText),
                                            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                                            {
                                                ComponentBuilderAttributeConfiguration.CreateConstantAttribute("ChildContent", HelpText),
                                            }
                                        }
                                    }
                                },
                                new ComponentBuilderConfiguration()
                                {
                                    ComponentType = typeof (MudIconButton),
                                    ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                                    {
                                        ComponentBuilderAttributeConfiguration.CreateConstantAttribute("Icon", Icons.Material.Filled.Close),
                                        ComponentBuilderAttributeConfiguration.CreateConstantAttribute("OnClick", () => ChangeOpenStatus(false)),
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
    }

    private void ChangeOpenStatus(bool open)
    {
        Console.WriteLine("Description: " + (string)(open ? "Opened" : "Closed"));
        _isOpened = open;
    }
    public void TryToAddChild(IFormElement child)
    {
        throw new NotSupportedException();
    }

    public void OpenDescription() => ChangeOpenStatus(true);

    public void CloseDescription() => ChangeOpenStatus(false);
}