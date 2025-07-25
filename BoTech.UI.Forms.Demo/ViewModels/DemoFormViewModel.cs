using Avalonia.Layout;
using BoTech.UI.Forms.Models;
using BoTech.UI.Forms.Models.PropertyAnnotations;

namespace BoTech.UI.Forms.Demo.ViewModels;
[FormInputGroupStack("Register", "Register to accounts.botech.dev", Orientation.Vertical)]
public class DemoFormViewModel : FormViewModelBase
{
    [FormResultProperty]
    public PersonalInformation PersonalInfo { get; set; }
    [FormResultProperty]
    public AddressDetails AddressInfo { get; set; }
    [FormInputGroupStack("PersonalInformation", "Your Personal Information", Orientation.Vertical)]
    public class PersonalInformation : FormViewModelBase
    {
        [FormInputProperty("FirstName", "Your First Name", "")]
        public string FirstName { get; set; }
        [FormInputProperty("LastName", "Your Last Name", "")]
        public string LastName { get; set; }
    }
    [FormInputGroupGrid("AddressDetails", "Your Address Details", 2,2)]
    public class AddressDetails : FormViewModelBase
    {
        [FormInputPropertyGridItem("Street", "The name of the street where you life", "", 0,0)]
        public string Street { get; set; }
        [FormInputPropertyGridItem("City", "The name of the City where you life", "", 1,0)]
        public string City { get; set; }
        [FormInputPropertyGridItem("State", "The name of the State where you life", "", 0,1)]
        public string State { get; set; }
        [FormInputPropertyGridItem("Zip", "The name of the Zip where you life", "", 1,1)]
        public string Zip { get; set; }
    }
}