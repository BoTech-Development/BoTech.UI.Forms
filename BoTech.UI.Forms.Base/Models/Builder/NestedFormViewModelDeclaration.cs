using BoTech.UI.Forms.Base.Models.PropertyAnnotations;

namespace BoTech.UI.Forms.Base.Models.Builder;
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
    /// Instance of the current FormViewModel is needed to save all data that the user entered in the form
    /// </summary>
    public FormViewModelBase Instance { get; set; }
    /// <summary>
    /// All properties that need an input elemnt.
    /// </summary>
    public List<FormProperty> DeclaredFormProperties { get; set; } = new List<FormProperty>();
    /// <summary>
    /// Save all information about the current group, such as the displayed name of the form group.
    /// </summary>
    public FormInputGroup GroupInformation { get; set; }
}