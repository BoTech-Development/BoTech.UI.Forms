namespace BoTech.UI.Forms.Base.Models.PropertyAnnotations;
/// <summary>
/// Declares a result property where a part of the nested class will be injected after the user clicks the Submit button.
/// </summary>
 [AttributeUsage(AttributeTargets.Property,  AllowMultiple = false)]
public class FormResultProperty : Attribute
{
    
}