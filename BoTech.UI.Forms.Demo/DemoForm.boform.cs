using BoTech.UI.Forms.Models;
using BoTech.UI.Forms.Models.PropertyAnnotations;

namespace BoTech.UI.Forms.Demo;

public class DemoForm
{
    public PersonalInformation PersonalInfo { get; set; }
   
    public AddressDetails AddressInfo { get; set; }
    public ContactDetails ContactInfo { get; set; }
    public StatisticDetails StatisticInfo { get; set; }
    public AcceptanceDetails AcceptanceInfo { get; set; }


    public class PersonalInformation : FormViewModelBase
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }

    public class ContactDetails : FormViewModelBase
    {
        public string Email { get; init; }
        public string Phone { get; init; }
    }

    public class AddressDetails : FormViewModelBase
    {
        public string Country { get; init; }
        public string City { get; init; }
        
        public string Street { get; init; }
        public int HouseNumber { get; init; }
        public string PostalCode { get; init; }
    }

    public class StatisticDetails : FormViewModelBase
    {
        public Profession Profession { get; init; }
        public int YearsOfExperience { get; init; }
        public int ProductRating { get; init; }
    }

    public class AcceptanceDetails : FormViewModelBase
    {
        public bool AcceptedTermsOfUse { get; init; }
        public bool AcceptedLicense { get; init; }
        public bool AcceptedToReceiveEmails { get; init; }
    }

    public enum Profession
    {
        NoAnswer,
        Freelancer,
        Scientist,
        SoftwareEngineer,
        TechnicalEngineer
    }
}