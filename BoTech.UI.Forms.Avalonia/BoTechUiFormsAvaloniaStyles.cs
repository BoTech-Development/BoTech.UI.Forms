using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace BoTech.UI.Forms.Avalonia;

public class BoTechUiFormsAvaloniaStyles : Styles
{
    public BoTechUiFormsAvaloniaStyles()
    {
        AvaloniaXamlLoader.Load(this);
    }
}