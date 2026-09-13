using System;
using System.Collections.Generic;
using System.Text;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Converter;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Avalonia.Rendering
{
    internal class ComponentAttributeFactory : IComponentAttributeFactory
    {
        public static IComponentBuilderAttributeConfiguration CreateConstantAttributeWithControlAsValue(string attributeName,
            IFormElement controlValueConfig)
        {
            return new ComponentBuilderAttributeConfiguration(attributeName, null, null, null, null, null, controlValueConfig);
        }

        public static IComponentBuilderAttributeConfiguration CreateConstantAttributeWithControlAsValue(string attributeName,
            IComponentBuilderConfiguration controlValueConfig)
        {
            return new ComponentBuilderAttributeConfiguration(attributeName, null, null, null, null, controlValueConfig, null);
        }

        public static IComponentBuilderAttributeConfiguration CreateConstantAttribute(string attributeName, object attributeValue)
        {
            return new ComponentBuilderAttributeConfiguration(attributeName, attributeValue, null, null, null, null, null);
        }

        public static IComponentBuilderAttributeConfiguration CreateBindingAttribute(string attributeNameInComponent,
            object defaultValue, string nameOfBindingProperty, Type? typeWhereTheBindingPropertyIsDeclared)
        {
            return new ComponentBuilderAttributeConfiguration(attributeNameInComponent, defaultValue,
                typeWhereTheBindingPropertyIsDeclared, null ,nameOfBindingProperty, null, null);
        }

        public static IComponentBuilderAttributeConfiguration CreateBindingAttribute(string attributeNameInComponent,
            object defaultValue, string nameOfBindingProperty, Type typeWhereTheBindingPropertyIsDeclared,
            Type typeOfValueConverterImplementation)
        {
            return new ComponentBuilderAttributeConfiguration(attributeNameInComponent, defaultValue,
                typeWhereTheBindingPropertyIsDeclared, typeOfValueConverterImplementation, nameOfBindingProperty, null, null);
        }
    }
}
