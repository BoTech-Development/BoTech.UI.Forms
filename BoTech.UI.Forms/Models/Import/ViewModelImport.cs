using BoTech.UI.Forms.Controls;

namespace BoTech.UI.Forms.Models.Import;
/// <summary>
/// You can use this class to declare a ViewModel input. A ViewModel is a class, that is connected to various input fields in the form.
/// </summary>
public class ViewModelImport : ImportBase
{
    /// <summary>
    /// The Namespace of the class without the class name
    /// </summary>
    public string Namespace { get; init; } = string.Empty;
    /// <summary>
    /// The name of the class 
    /// </summary>
    public string ClassName { get; init; } = string.Empty;
    /// <summary>
    /// A name that must be used to reference the properties of a ViewModel.
    /// </summary>
    public string As { get; init; } = string.Empty;

}