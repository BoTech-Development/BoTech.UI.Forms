using BoTech.UI.Controls.Forms;
using BoTech.UI.Forms.Base.Models.Builder.Form;

namespace BoTech.UI.Forms.Base.Models.PropertyAnnotations;

public class NumericFormInputProperty : FormInputProperty
{
    public NumberFormInput.NumericUpDownConfiguration Configuration { get; }
    /// <summary>
    /// Configures a property as a form input which will be an Numeric up down but with a custom Configuration.
    /// </summary>
    /// <param name="name">The Name of the Property (The name that should be displayed.</param>
    /// <param name="helpText">More Information that will be displayed when the user hovers over the Help Button.</param>
    /// <param name="configuration">The Configuration of the Numeric up Down</param>
    public NumericFormInputProperty(string name, string helpText, NumberFormInput.NumericUpDownConfiguration configuration) : base(name, helpText, configuration.Value)
    {
        Configuration = configuration;
    }
}