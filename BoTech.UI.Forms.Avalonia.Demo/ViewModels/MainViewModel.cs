using System;
using System.Collections.Generic;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Data;
using BoTech.UI.Forms.Avalonia.Controls.Input.Text;
using BoTech.UI.Forms.Avalonia.Controls.Layout;
using BoTech.UI.Forms.Avalonia.Rendering;
using BoTech.UI.Forms.Controls.Layout;
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
        Stack stackPanel = new Stack()
        {
            Orientation = Orientation.Horizontal
        };
        TextInput textInput = new TextInput()
        {
            Name = "FirstName",
            Description = "We need your first name for identification purpose",
            IsMultiline = true,

        };
        SearchTextInput searchTextInput = new SearchTextInput()
        {
            Name = "Country",
            Description = "We need your country for identification purpose",
            ItemSource = new List<string>()
            {
                "Germany",
                "France",
                "Italy",
                "Spain",
                "USA",
                "England",
                "Finland",

            },
            SortByRegex = "^F"
        };
        stackPanel.Children.Add(textInput);
        stackPanel.Children.Add(searchTextInput);
        OnOpenDescription = ReactiveCommand.Create(() =>
        {
            textInput.OpenDescription();
        });
        
        surface.UpdateRootElement(stackPanel);
        surface.Render();
        Form = (Control)surface.GetPrerenderedRootElement();
    }
}