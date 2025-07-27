using System.Collections.Generic;
using Avalonia.Controls;
using BoTech.UI.Controls.Forms;
using BoTech.UI.Forms;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Models;
using BoTech.UI.Forms.Models.Builder;
using ReactiveUI;

namespace BoTech.UI.Forms.Demo.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private FormControl _formControl;
    public FormControl FormControl 
    { 
        get => _formControl; 
        set => this.RaiseAndSetIfChanged(ref _formControl, value);
        
    }
    public MainWindowViewModel()
    {
        FormControl form = new FormBuilder().CreateFormFor(new DemoFormViewModel());
        form.OnFormAccepted += OnFormAccepted;
        form.OnFormCancelled += OnFormCancelled;
        form.OnFormRequiredInputsMissing += OnFormRequiredInputsMissing;
        FormControl = form;
    }

    private void OnFormRequiredInputsMissing(FormViewModelBase currentStatusOrResult, List<FormProperty> missingInputs, FormResultOption result)
    {
        //throw new System.NotImplementedException();
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