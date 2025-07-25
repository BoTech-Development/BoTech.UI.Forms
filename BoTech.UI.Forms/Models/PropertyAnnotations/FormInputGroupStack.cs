using Avalonia.Controls;
using Avalonia.Layout;

namespace BoTech.UI.Forms.Models.PropertyAnnotations;
/// <summary>
/// Defines a group which will be displayed with a StackPanel
/// </summary>
[AttributeUsage(AttributeTargets.Class,  AllowMultiple = false,  Inherited = true)]
public class FormInputGroupStack : FormInputGroup 
{
    /// <summary>
    /// Defines the Orientation of the StackPanel which will be created by the FormBuilder and the FormPresenter Control
    /// </summary>
    public Orientation StackOrientation { get;}
   
    /// <summary>
    /// Defines a new Group of InputFields which will be displayed in a StackPanel.
    /// </summary>
    /// <param name="groupName">The Unique ID of this Group</param>
    /// <param name="stackOrientation">Orientation of the Stackpanel</param>
    /// <param name="groupLabel">A string which will be displayed above the Groupo</param>
    public FormInputGroupStack(string groupName, string groupLabel, Orientation stackOrientation) : base(groupName, groupLabel)
    {
       StackOrientation = stackOrientation;
       
    }
}