using BoTech.UI.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using BoTech.UI.Forms.Converter;
using BoTech.UI.Forms.Models;

namespace BoTech.UI.Forms.Rendering
{
    public interface IComponentAttributeFactory
    {
        /// <summary>
        /// This method allows a control attribute to be created.
        /// It constructs the passed control and sets the specified property to the value of the rendered control.
        /// The property type varies depending on the package (Avalonia, Web, etc.).
        /// </summary>
        /// <param name="attributeName">The name of the attribute in the control to set.</param>
        /// <param name="controlValueConfig">the value to set</param>
        /// <returns>the config which will be applied with the <see cref="IComponentBuilder{TControlTypeBase}"/></returns>
        public static abstract IComponentBuilderAttributeConfiguration CreateConstantAttributeWithControlAsValue(string attributeName, IFormElement controlValueConfig);
        /// <summary>
        /// This method allows a control attribute to be created.
        /// It constructs the passed control and sets the specified property to the value of the rendered control.
        /// The property type varies depending on the package (Avalonia, Web, etc.).
        /// </summary>
        /// <param name="attributeName">The name of the attribute in the control.</param>
        /// <param name="controlValueConfig">the value to set</param>
        /// <returns>the config which will be applied with the <see cref="IComponentBuilder{TControlTypeBase}"/></returns>
        public static abstract IComponentBuilderAttributeConfiguration CreateConstantAttributeWithControlAsValue(string attributeName, IComponentBuilderConfiguration controlValueConfig);
        /// <summary>
        /// This method can be used to create a constant attribute for a specific <see cref="IComponentBuilderConfiguration"/>
        /// </summary>
        /// <param name="attributeName">The name of the attribute to set</param>
        /// <param name="attributeValue"> the value to set</param>
        /// <returns>the config which will be applied with the <see cref="IComponentBuilder{TControlTypeBase}"/></returns>
        public static abstract IComponentBuilderAttributeConfiguration CreateConstantAttribute(string attributeName, object attributeValue);
        /// <summary>
        /// Creates a Binding between the <paramref name="nameOfBindingProperty"/> defined in the class <paramref name="typeWhereTheBindingPropertyIsDeclared"/> and the Attribute in the blazor or Avalonia control.
        /// </summary>
        /// <param name="attributeNameInComponent">The name of the property in the actual control class (e.g. Avalonia, MudBlazor)</param>
        /// <param name="defaultValue">The default value of the attribute.</param>
        /// <param name="nameOfBindingProperty">The name of the property in the Wrapper class</param>
        /// <param name="typeWhereTheBindingPropertyIsDeclared">The actual type of the control in the for instance avalonia package</param>
        /// <returns></returns>
        public static abstract IComponentBuilderAttributeConfiguration CreateBindingAttribute(string attributeNameInComponent, object defaultValue, string nameOfBindingProperty, Type typeWhereTheBindingPropertyIsDeclared);

        /// <summary>
        /// Creates a Binding between the <paramref name="nameOfBindingProperty"/> defined in the class <paramref name="typeWhereTheBindingPropertyIsDeclared"/> and the Attribute in the blazor or Avalonia control.
        /// When the value is set, it is converted to the destination type using the <see cref="typeOfValueConverterImplementation"/>.
        /// </summary>
        /// <param name="attributeNameInComponent">The name of the property in the actual control class (e.g. Avalonia, MudBlazor)</param>
        /// <param name="defaultValue">The default value of the attribute.</param>
        /// <param name="nameOfBindingProperty">The name of the property in the Wrapper class</param>
        /// <param name="typeWhereTheBindingPropertyIsDeclared">The actual type of the control in the for instance avalonia package</param>
        /// <param name="typeOfValueConverterImplementation">Type of the converter</param>
        /// <returns></returns>
        public static abstract IComponentBuilderAttributeConfiguration
            CreateBindingAttribute(string attributeNameInComponent,
                object defaultValue,
                string nameOfBindingProperty, 
                Type typeWhereTheBindingPropertyIsDeclared, 
                Type typeOfValueConverterImplementation);
    }
}
