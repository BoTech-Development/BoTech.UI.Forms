using System.Reflection;
using BoTech.UI.Forms.Models.PropertyAnnotations;

namespace BoTech.UI.Forms.Models.Builder;

public class FormProperty(PropertyInfo property, FormInputProperty annotation)
{
    public PropertyInfo Property { get; } =  property;
    public FormInputProperty Annotation { get; } = annotation;
}