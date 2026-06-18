using System;
using System.Collections.Generic;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Data;
using BoTech.UI.Forms.Avalonia.Controls.Input.DateTime;
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
            Orientation = Orientation.Vertical
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
        TimeInput timeInput = new TimeInput()
        {
            Name = "When do you started your PC today?",
            Description = "Please enter the time in 24H format when you started (turned on) your computer today.",
        };
        DateInput dateInput = new DateInput()
        {
            Name = "Birthday",
            Description = "We need your birthday for confirming your age.",
        };
        dateInput.SetToCurrentDate();
        DateTimeInput dateTimeInput = new DateTimeInput()
        {
            Name = "Preferred appointment",
            Description = "Your new dentist appointment"
        };
        timeInput.SetToCurrentTime();
        stackPanel.Children.Add(textInput);
        stackPanel.Children.Add(searchTextInput);
        stackPanel.Children.Add(timeInput);
        stackPanel.Children.Add(dateInput);
        stackPanel.Children.Add(dateTimeInput);
        OnOpenDescription = ReactiveCommand.Create(() =>
        {
            textInput.OpenDescription();
          //  searchTextInput.OpenDescription();
           // timeInput.OpenDescription();
            Console.WriteLine(timeInput.Value.ToString());
        });
        
        surface.UpdateRootElement(stackPanel);
        surface.Render();
        Form = (Control)surface.GetPrerenderedRootElement();
    }
}