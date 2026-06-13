namespace BoTech.UI.Forms.Rendering;

public class ComponentBuilderAttributeConfiguration 
{
    /// <summary>
    /// The type where the property is defined which should be connected over a Binding to the actual Component Property.
    /// null when this <see cref="ComponentBuilderAttributeConfiguration"/> is not a binding.
    /// </summary>
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
    /// </summary>
    public object AttributeValue { get; init; }
    /// <summary>
    /// When the property should be set to a control.
    /// It is necessary to build the Control too.
    /// </summary>
    public IComponentBuilderConfiguration? ControlValueConfig { get; init; }
    public bool HasAnotherControlAsValue => AttributeValue is IComponentBuilderConfiguration && ControlValueConfig != null;
    public bool IsBindingProperty  => TypeWhereTheBindingPropertyIsDeclared != null && NameOfBindingProperty != null;
    
    private ComponentBuilderAttributeConfiguration(string attributeName, object attributeValue, Type? typeWhereTheBindingPropertyIsDeclared, string? nameOfBindingProperty)
    {
        AttributeName =  attributeName;
        AttributeValue = attributeValue;
        TypeWhereTheBindingPropertyIsDeclared = typeWhereTheBindingPropertyIsDeclared;
        NameOfBindingProperty = nameOfBindingProperty;
        
    }
    private ComponentBuilderAttributeConfiguration(string attributeName, IComponentBuilderConfiguration attributeValue)
    {
        AttributeName =  attributeName;
        ControlValueConfig = attributeValue;
        TypeWhereTheBindingPropertyIsDeclared = null;
        NameOfBindingProperty = null;
        AttributeValue = attributeValue;
    }

    public static ComponentBuilderAttributeConfiguration CreateConstantAttributeWithControlAsValue(string attributeName,
        IComponentBuilderConfiguration controlValueConfig)
    {
        
        return new ComponentBuilderAttributeConfiguration(attributeName, controlValueConfig);
    }
    /// <summary>
    /// This method can be used to create a constant attribute for a specific <see cref="IComponentBuilderConfiguration"/>
    /// </summary>
    /// <param name="attributeName">The name of the attribute to set</param>
    /// <param name="attributeValue"> the value to set</param>
    /// <returns>the config which will be applied with the <see cref="IComponentBuilder{TControlTypeBase}"/></returns>
    public static ComponentBuilderAttributeConfiguration CreateConstantAttribute(string attributeName,
        object attributeValue)
    {
        return new ComponentBuilderAttributeConfiguration(attributeName, attributeValue, null, null);
    }
    /// <summary>
    /// Creates a Binding between the <paramref name="nameOfBindingProperty"/> defined in the class <paramref name="typeWhereTheBindingPropertyIsDeclared"/> and the Attribute in the blazor or Avalonia control.
    /// </summary>
    /// <param name="attributeNameInComponent"></param>
    /// <param name="defaultValue">The default value of the attribute.</param>
    /// <param name="nameOfBindingProperty"></param>
    /// <param name="typeWhereTheBindingPropertyIsDeclared"></param>
    /// <returns></returns>
    public static ComponentBuilderAttributeConfiguration CreateBindingAttribute(string attributeNameInComponent,
        object defaultValue, string nameOfBindingProperty, Type? typeWhereTheBindingPropertyIsDeclared)
    {
        return new ComponentBuilderAttributeConfiguration(attributeNameInComponent, defaultValue, typeWhereTheBindingPropertyIsDeclared, nameOfBindingProperty);
    }

    public override string ToString()
    {
        string result = "{ComponentBuilderAttributeConfiguration ";
        if (HasAnotherControlAsValue)
        {
            result += $"ConstantWithChildControl: AttributeName: {AttributeName} AttributeValue: {AttributeValue}";
        }
        else if (IsBindingProperty)
        {
            result += $"Binding: From: ({TypeWhereTheBindingPropertyIsDeclared}).({NameOfBindingProperty}) To: {AttributeName} with default value: AttributeValue: {AttributeValue}";
        }
        else
        {
            result += $"ConstantPrimitive: AttributeName: {AttributeName} AttributeValue: {AttributeValue}";
        }
        return result + "}";
    }
}