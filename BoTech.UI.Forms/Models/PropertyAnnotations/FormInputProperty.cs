namespace BoTech.UI.Forms.Models.PropertyAnnotations;
[AttributeUsage(AttributeTargets.Property,  AllowMultiple = false)]
public class FormInputProperty : Attribute
{
    public string Name { get; }
    public string HelpText { get; }
    public object? DefaultValue { get; }
    /// <summary>
    /// Configures a property as a form input.
    /// </summary>
    /// <param name="name">The Name of the Property (The name that should be displayed.</param>
    /// <param name="helpText">More Information that will be displayed when the user hovers over the Help Button.</param>
    /// <param name="defaultValue">When null no default value</param>
    public FormInputProperty(string name, string helpText, object? defaultValue)
    {
        Name = name;
        HelpText = helpText;
        DefaultValue = defaultValue;
    }
    
}