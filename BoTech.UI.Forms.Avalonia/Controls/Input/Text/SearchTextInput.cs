using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Input.Text;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Avalonia.Controls.Input.Text;

public class SearchTextInput : ISearchTextInput
{
    public bool IsVisible { get; set; }
    public bool IsEnabled { get; set; }
    public string Name { get; init; }
    public Guid Id { get; init; }
    public string Property { get; init; }
    public string Value { get; }
    public event EventHandler? OnUserUpdatedValue;
    public bool IsMultiline { get; init; }
    public string StaticItemSource { get; set; }
    public IEnumerable<string> ItemSource { get; set; }
    public string SortByRegex { get; set; }
    public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
    {
        throw new NotImplementedException();
    }
    
    public void TryToAddChild(IFormElement child)
    {
        throw new NotImplementedException();
    }

    public IHelpDescriptionOfFormElement HelpDescriptionOfFormElement { get; }
    public string Description { get; init; }
    public void OpenDescription()
    {
        throw new NotImplementedException();
    }

    public void CloseDescription()
    {
        throw new NotImplementedException();
    }



}