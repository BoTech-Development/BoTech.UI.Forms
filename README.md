![BoTech Logo](https://raw.githubusercontent.com/BoTech-Development/BoTech.UI/master/ReadmeAssets/BoTechLogoComplete.png)

# BoTech.UI.Forms
#### Please note: This Project is in EAP (Early Access Mode) or Beta mode =>There may be serious errors in the software or functions may be only partially implemented. 

## What is this?
+ This extension makes it easier to create input forms. All you need to do is create a ViewModel, add a few annotations, and tell my library to create a form for that ViewModel.

## General structure
+ We use a ViewModel to describe the appearance of the form and to store all the data that has been entered. 
+ The library takes care of validating and styling the view.

## Result
https://github.com/user-attachments/assets/5a0da46b-1ecb-45e9-bb9e-1747f6eedbdd

### ViewModel
![Demo ViewModel](https://raw.githubusercontent.com/BoTech-Development/BoTech.UI.Forms/master/ReadmeAssets/DemoViewModel.png)


### Installation 
````bash
dotnet add package BoTech.UI.Forms
dotnet add package BoTech.UI
dotnet add package Material.Icons.Avalonia
````

#### App.axaml

````xaml
    <Application.Styles>
        <FluentTheme />
        <avalonia:MaterialIconStyles />
        <botechui:BoTechTheme/>
    </Application.Styles>
````

#### MainWindow.axaml
````xaml
<ContentControl Content="{Binding FormControl}"/>
````

#### MainWindowViewModel.cs
````c#
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
````
#### {YourName}FormViewModel.cs
````c#
[FormInputGroupStack("Register", "Register to accounts.botech.dev", Orientation.Vertical)]
public class {YourName}ViewModel : FormViewModelBase
{
    [FormResultProperty]
    public PersonalInformation PersonalInfo { get; set; }
    [FormResultProperty]
    public AddressDetails AddressInfo { get; set; }
    [FormInputGroupStack("PersonalInformation", "Your Personal Information", Orientation.Vertical)]
    public class PersonalInformation : FormViewModelBase
    {
        [FormInputProperty("FirstName", "Your First Name", true,"")]
        public string FirstName { get; set; }
        [FormInputProperty("LastName", "Your Last Name",true, "")]
        public string LastName { get; set; }
    }
    [FormInputGroupGrid("AddressDetails", "Your Address Details", 2,2)]
    public class AddressDetails : FormViewModelBase
    {
        [FormInputPropertyGridItem("Street", "The name of the street where you life",false ,"", 0,0)]
        public string Street { get; set; }
        [FormInputPropertyGridItem("City", "The name of the City where you life", false,"", 1,0)]
        public string City { get; set; }
        [FormInputPropertyGridItem("State", "The name of the State where you life", false,"", 0,1)]
        public string State { get; set; }
        [FormInputPropertyGridItem("Zip", "The name of the Zip where you life", false,"", 1,1)]
        public string Zip { get; set; }
    }
}
````
+ Your ViewModel class must inherit from the ***FormViewModelBase*** class so that the system knows that this is a ViewModel of a Form.
+ The ViewModel must be provided with so-called annotations so that it is clear what the layout of the form should be and to help display text and other information.
+ The annotation ***FormInputGroupStack*** defines that all properties (FormInputs) of the class should be arranged in a StackPanel.
+ The ***FormInputProperty*** annotation defines an input field. The input field adapts to the property type (not all types are supported yet). It also specifies the text that should be displayed when the question mark is clicked, as well as the property name.
  + By setting the ***isRequired*** property to true, you can specify that the user must provide input. If the user doesn't, the OnFormRequiredInputsMissing event is fired when the cancel or accept button is clicked. Otherwise, either the OnFormAccepted event or the OnFormCancelled event is fired.
+ If you want to use a ***grid*** as a layout, you must provide your group of properties (class) with the ***FormInputGroupGrid*** annotation and all properties with the ***FormInputPropertyGridItem*** annotation, since additional data such as Row and Column must be specified here.
+ A ***class*** defines a group of input options.