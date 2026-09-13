using System.Reflection;
using System.Text.Json.Serialization;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Converter;

namespace BoTech.UI.Forms.Rendering;
/// <summary>
/// This class (interface) describes how the <see cref="IComponentBuilder{TControlTypeBase}"/> should set any property of the actual control.
/// There are the following options: Setting the property to a constant value; Create a binding between the wrapper class and the actual property defined in the control (there is the option to use the <see cref="IConverter{TSource,TDestination}"/> interface); Set the property value to a component configuration or to the build result of a wrapper control class.
/// Because of some Property values needs to be converted before setting, there is an option to pass the implementation of <see cref="IConverter{TSource,TDestination}"/>.
/// </summary>
public interface IComponentBuilderAttributeConfiguration
{
    /// <summary>
    /// Value for serializing <see cref="TypeWhereTheBindingPropertyIsDeclared"/>
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public string FullComponentTypeName { get; init; }
    /// <summary>
    /// The type where the property is defined which should be connected over a Binding to the actual Component Property.
    /// null when this <see cref="IComponentBuilderAttributeConfiguration"/> is not a binding.
    /// </summary>
    [JsonIgnore]
    public Type? TypeWhereTheBindingPropertyIsDeclared { get; init; }
    /// <summary>
    /// The name of the property which is defined in the <see cref="TypeWhereTheBindingPropertyIsDeclared"/> Type.
    /// </summary>
    public string? NameOfBindingProperty { get; init; }
    /// <summary>
    /// The name of the attribute that should be set in the Component.
    /// </summary>
    public string AttributeName { get; init; }
    /// <summary>
    /// the value of the attribute that should be set in the Component.
    /// Can be null when <see cref="FormElementAsValue"/> or <see cref="ControlValueConfig"/> is not null.
    /// </summary>
    public object? AttributeValue { get; init; }
    /// <summary>
    /// When the property should be set to a control, which can be built by configuration
    /// It is necessary to build the Control too.
    /// </summary>
    public IComponentBuilderConfiguration? ControlValueConfig { get; init; }
    /// <summary>
    /// When the property should be set to an actual FormElement.
    /// It is necessary to build the Control too, before setting the property.
    /// </summary>
    public IFormElement? FormElementAsValue { get; set; }
    /// <summary>
    /// You can use this property to define the type of the <see cref="IConverter{TSource,TDestination}"/> implementation.
    /// This Converter will be used by setting and getting the value.
    /// </summary>
    public Type? TypeOfValueConverter { get; }
    /// <summary>
    /// true when the user wants to create a component and insert it into a specific property in the current Component.
    /// </summary>
    public bool HasAnotherControlAsValue { get; }
    /// <summary>
    /// Only usable when <see cref="HasAnotherControlAsValue"/> is true.
    /// This value is true, when <see cref="FormElementAsValue"/> is not null.
    /// </summary>
    public bool IsAnotherControlAFormElement { get; }
    /// <summary>
    /// true when the attribute should be a binding with mode two-way.
    /// </summary>
    public bool IsBindingProperty { get; }
}