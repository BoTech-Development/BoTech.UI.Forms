using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Input.Text;
using BoTech.UI.Forms.Controls.Layout;
using BoTech.UI.Forms.Rendering;
using BoTech.UI.Forms.Web.Rendering;
using MudBlazor;

namespace BoTech.UI.Forms.Web.Controls.Input.Text;

public class TextInput : ITextInput
{
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    public string Name { get; init; }
    public Guid Id { get; init; }
    public string Description { get; init; }
    public string Property { get; init; }
    public bool IsMultiline { get; init; }
    public string Value { get; }
    public event EventHandler? OnUserUpdatedValue;
    
    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        return new ComponentBuilderConfiguration()
        {
            ComponentType = typeof(MudStack),
            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
            {
                ComponentBuilderAttributeConfiguration.CreateConstantAttribute("Row", true)
            },
            Children = new List<IComponentBuilderConfiguration>()
            {
                new ComponentBuilderConfiguration()
                {
                    ComponentType = typeof(MudTextField<string>),
                    ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                    {
                        ComponentBuilderAttributeConfiguration.CreateConstantAttribute("HelperText", "Some Name")
                    }
                },
                new HelpDescriptionOfFormElement()
                {
                    HelpText = Description,
                }.BuildComponentBuilderConfigurationFromThis()
            }
        };
        /* return new ComponentBuilderConfiguration()
         {
             ComponentType = typeof(MudStack),
             ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
             {
                 new ComponentBuilderAttributeConfiguration("Row",true)
             },
             Children = new List<ComponentBuilderConfiguration>()
             {
                 new ComponentBuilderConfiguration()
                 {
                     ComponentType = typeof(MudTextField<string>),
                     ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                     {
                         new ComponentBuilderAttributeConfiguration("HelperText","Some Name")
                     }
                 },
                 new ComponentBuilderConfiguration()
                 {
                     ComponentType = typeof(MudToggleIconButton),
                     ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                     {
                         new ComponentBuilderAttributeConfiguration("Icon", Icons.Material.Filled.Info),
                         new ComponentBuilderAttributeConfiguration( "ToggledIcon", Icons.Material.Filled.Close)
                     }
                 }
             }
         };*/
    }
    
    public void TryToAddChild(IFormElement child)
    {
        throw new NotSupportedException("Cannot add child to a TextInput control.");
    }

    public void OpenDescription()
    {
        throw new NotImplementedException();
    }

    public void CloseDescription()
    {
        throw new NotImplementedException();
    }
}