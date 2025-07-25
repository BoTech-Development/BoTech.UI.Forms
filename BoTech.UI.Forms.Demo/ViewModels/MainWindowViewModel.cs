using Avalonia.Controls;
using BoTech.UI.Controls.Forms;
using BoTech.UI.Forms;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Models;
using ReactiveUI;

namespace BoTech.UI.Forms.Demo.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
    private FormControl _formControl;
    public FormControl FormControl 
    { 
        get => _formControl; 
        set => this.RaiseAndSetIfChanged(ref _formControl, value);
        
    }
    private Control _testControl;
    public Control TestControl 
    { 
        get => _testControl; 
        set => this.RaiseAndSetIfChanged(ref _testControl, value);
        
    }

    public MainWindowViewModel()
    {
        FormControl form = new FormBuilder().CreateFormFor(new DemoFormViewModel());
        form.OnFormAccepted += OnFormAccepted;
        form.OnFormCancelled += OnFormCancelled;
        FormControl = form;
        TestControl = new TextFormInput("FirstName", "The First Name", "Florian");
    }

    private void OnFormCancelled(FormViewModelBase currentStatus)
    {
        //throw new System.NotImplementedException();
    }

    private void OnFormAccepted(FormViewModelBase result)
    {
        //throw new System.NotImplementedException();
    }
}