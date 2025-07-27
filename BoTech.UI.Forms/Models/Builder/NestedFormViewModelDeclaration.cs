using System.Reflection;
using BoTech.UI.Forms.Models.PropertyAnnotations;

namespace BoTech.UI.Forms.Models.Builder;
/// <summary>
/// Represents a class structure of an FormViewModelBase.
/// </summary>
public class NestedFormViewModelDeclaration
{
    /// <summary>
    /// All Sub classes or sub FormViewModels
    /// </summary>
    public List<NestedFormViewModelDeclaration> SubViewModels { get; set; } = new List<NestedFormViewModelDeclaration>();
    /// <summary>
    /// Type of the current Class
    /// </summary>
    public Type ViewModelType { get; set; }
    
    /// <summary>
    /// All properties that need an input elemnt.
    /// </summary>
    public List<FormProperty> DeclaredFormProperties { get; set; } = new List<FormProperty>();
    /// <summary>
    /// Saves all properties that are used to inject the instance of the SubClasses.
    /// </summary>
    public List<PropertyInfo> FormResultProperties { get; set; } = new List<PropertyInfo>();
    /// <summary>
    /// Save all information about the current group, such as the displayed name of the form group.
    /// </summary>
    public FormInputGroup GroupInformation { get; set; }
}