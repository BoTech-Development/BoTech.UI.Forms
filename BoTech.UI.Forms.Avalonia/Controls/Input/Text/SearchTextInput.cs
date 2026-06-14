using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using BoTech.UI.Forms.Avalonia.Rendering;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Input.Text;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Avalonia.Controls.Input.Text;

public class SearchTextInput : ISearchTextInput
{
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    public string Name { get; init; }
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Property { get; init; }
    public string Value { get; }
    public event EventHandler? OnUserUpdatedValue;
    public bool IsMultiline { get; init; }
    public string StaticItemSource { get; set; }
    public IEnumerable<string> ItemSource { get; set; }
    public string SortByRegex { get; set; }
    public IHelpDescriptionOfFormElement HelpDescriptionOfFormElement { get; private set; }
    public string Description { get; init; }
    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        HelpDescriptionOfFormElement = new HelpDescriptionOfFormElement()
        {
            IsEnabled = this.IsEnabled,
            IsVisible = this.IsVisible,
            HelpText = this.Description,
        };
        Regex regex = new Regex(SortByRegex);
        return new ComponentBuilderConfiguration(this)
        {
            ComponentType = typeof(StackPanel),
            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
            {
                ComponentBuilderAttributeConfiguration.CreateConstantAttribute("Orientation", Orientation.Horizontal),
            },
           
            Children = new List<IComponentBuilderConfiguration>()
            {
                new InputNameLeftOfInput()
                {
                    Name = this.Name,
                }.BuildComponentBuilderConfigurationFromThis(),
                new ComponentBuilderConfiguration(this)
                {
                    ComponentType = typeof(AutoCompleteBox),
                    ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                    {
                        ComponentBuilderAttributeConfiguration.CreateBindingAttribute("SelectedItem", Value, "Value",
                            typeof(TextInput)),
                        ComponentBuilderAttributeConfiguration.CreateConstantAttribute("ItemsSource",  ItemSource),
                      /*  ComponentBuilderAttributeConfiguration.CreateConstantAttribute("ItemFilter", (string search, string item) =>
                        {
                            if (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
                                return true;
                            return regex.IsMatch(search);
                        }),*/
                    }
                },
                HelpDescriptionOfFormElement.BuildComponentBuilderConfigurationFromThis()
            }
        };
    }
    
    public void TryToAddChild(IFormElement child)
    {
        throw new NotSupportedException();
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