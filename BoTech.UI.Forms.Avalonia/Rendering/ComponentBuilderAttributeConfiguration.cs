using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Converter;
using BoTech.UI.Forms.Rendering;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BoTech.UI.Forms.Avalonia.Rendering
{
    public class ComponentBuilderAttributeConfiguration (
        string attributeName,
        object attributeValue,
        Type? typeWhereTheBindingPropertyIsDeclared,
        Type? typeOfValueConverter,
        string? nameOfBindingProperty,
        IComponentBuilderConfiguration? controlValueConfig,
        IFormElement? formElementAsValue)
        : IComponentBuilderAttributeConfiguration
    {
        public string FullComponentTypeName
        {
            get
            {
                if (TypeWhereTheBindingPropertyIsDeclared != null && TypeWhereTheBindingPropertyIsDeclared.FullName != null)
                    field = TypeWhereTheBindingPropertyIsDeclared.FullName;
                return field;
            }
            init
            {
                Type? foundType = Assembly.GetExecutingAssembly().GetType(value);
                if (foundType != null)
                {
                    TypeWhereTheBindingPropertyIsDeclared = foundType;
                    field = foundType.FullName;
                }
                else
                    throw new ArgumentException($"Could not find type {value}");
            }
        } = "";
        public Type? TypeWhereTheBindingPropertyIsDeclared { get; init; } = typeWhereTheBindingPropertyIsDeclared;
        public string? NameOfBindingProperty { get; init; } = nameOfBindingProperty;
        public string AttributeName { get; init; } = attributeName;
        public object? AttributeValue { get; init; } = attributeValue;
        public IComponentBuilderConfiguration? ControlValueConfig { get; init; } = controlValueConfig;
        public IFormElement? FormElementAsValue { get; set; } = formElementAsValue;
        public Type? TypeOfValueConverter { get; } = typeOfValueConverter;
        public bool HasAnotherControlAsValue => ControlValueConfig != null || FormElementAsValue != null;
        public bool IsAnotherControlAFormElement => FormElementAsValue != null;
        public bool IsBindingProperty => TypeWhereTheBindingPropertyIsDeclared != null && NameOfBindingProperty != null;


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
            return result +"}";
        }
    }
}
