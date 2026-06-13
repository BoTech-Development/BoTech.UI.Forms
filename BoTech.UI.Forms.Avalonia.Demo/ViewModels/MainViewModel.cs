using System;
using Avalonia.Controls;
using Avalonia.Data;
using BoTech.UI.Forms.Avalonia.Controls.Input.Text;
using BoTech.UI.Forms.Avalonia.Rendering;

namespace BoTech.UI.Forms.Avalonia.Demo.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
    public Control Content { get; set; }
    public Control Form { get; set; }

    public MainViewModel()
    {
        
        TextBox box = new TextBox();
        var binding = new Binding("Greeting")
        {
            Mode = BindingMode.TwoWay,
        };
        box.Bind(TextBox.TextProperty, binding);
        Content = box;
        
        VisualSurface surface = new VisualSurface();
        surface.UpdateRootElement(new TextInput()
        {
            Description = "My description",
            IsMultiline =  true,

        });
        surface.Render();
        Form = surface.GetPrerenderedRootElement();
    }
}