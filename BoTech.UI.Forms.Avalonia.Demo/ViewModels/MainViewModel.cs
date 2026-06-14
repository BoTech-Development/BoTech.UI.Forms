using System;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Data;
using BoTech.UI.Forms.Avalonia.Controls.Input.Text;
using BoTech.UI.Forms.Avalonia.Rendering;
using ReactiveUI;

namespace BoTech.UI.Forms.Avalonia.Demo.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
    public Control Content { get; set; }
    public Control Form { get; set; }
    public ReactiveCommand<Unit, Unit> OnOpenDescription { get; set; }
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
        TextInput textInput = new TextInput()
        {
            Name = "FirstName",
            Description = "We need your first name for identification purpose",
            IsMultiline = true,

        };

        OnOpenDescription = ReactiveCommand.Create(() =>
        {
            textInput.OpenDescription();
        });
        
        surface.UpdateRootElement(textInput);
        surface.Render();
        Form = (Control)surface.GetPrerenderedRootElement();
    }
}