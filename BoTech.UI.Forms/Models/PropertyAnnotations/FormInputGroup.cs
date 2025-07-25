namespace BoTech.UI.Forms.Models.PropertyAnnotations;
/// <summary>
/// DO NOT USE THIS ANNOTATION IN YOUR MODEL USE <see cref="FormInputGroupGrid"/> OR <see cref="FormInputGroupStack"/> INSTEAD.
/// the form input group can be used to combine multiple form inputs into a specific layout.
/// </summary>
[AttributeUsage(AttributeTargets.Class,  AllowMultiple = false)]
public class FormInputGroup : Attribute
{
    public string GroupName { get; }
    public string GroupLabel { get; }
    /// <summary>
    /// This Constructor adds an input to a specific group.
    /// </summary>
    /// <param name="groupName"></param>
    public FormInputGroup(string groupName,  string groupLabel)
    {
        GroupName = groupName;
        GroupLabel = groupLabel;
    }
}