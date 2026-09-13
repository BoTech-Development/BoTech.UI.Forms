using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Layout;
using BoTech.UI.Forms.Converter;
using BoTech.UI.Forms.Rendering;
using BoTech.UI.Forms.Services;

namespace BoTech.UI.Forms.Avalonia.Rendering;

public class ComponentBuilder : IComponentBuilder<AvaloniaObject>
{
    public AvaloniaObject BuildComponent(IFormElement instanceOfRootFormElement)
    {
       // _currentFormElement = instanceOfRootFormElement;
        return BuildSpecificComponentFromConfigAndChildren(instanceOfRootFormElement, instanceOfRootFormElement.BuildComponentBuilderConfigurationFromThis());
    }

    private AvaloniaObject BuildSpecificComponentFromConfigAndChildren(IFormElement instanceOfRootFormElement, IComponentBuilderConfiguration config)
    {
        AvaloniaObject control = BuildSpecificComponentFromConfig(instanceOfRootFormElement, config);
        BuildChildrenOfFormElementAndAddToParent(instanceOfRootFormElement, control);
        return control;
    }
    private AvaloniaObject BuildSpecificComponentFromConfig(IFormElement instanceOfRootFormElement, IComponentBuilderConfiguration config)
    {
        AvaloniaObject avaloniaObject = (AvaloniaObject)config.ComponentType.GetConstructor(new Type[0]).Invoke(new object?[0]);
        
        //TODO: Remove side effect:
        VisualSurfaceManager<AvaloniaObject>.Instance.CurrentVisualSurface.GetRenderedComponentFinder().AddRenderedComponent(config.Id, instanceOfRootFormElement, avaloniaObject);
        
        if(avaloniaObject is Control control) 
            control.DataContext = instanceOfRootFormElement;
        AddComponentAttributesToControl(avaloniaObject, config, instanceOfRootFormElement);
        foreach (IComponentBuilderConfiguration child in config.Children)
        {
            AvaloniaObject childControl = BuildSpecificComponentFromConfig(instanceOfRootFormElement, child);
            TryToAddChildControlToParentControl(avaloniaObject, childControl);
        }
        return avaloniaObject;
    }

    private void BuildChildrenOfFormElementAndAddToParent(IFormElement formElement, AvaloniaObject parentControl)
    {
        if (formElement is IContentElement contentElement && contentElement.Content is not null)
        {
            AvaloniaObject childControl = BuildSpecificComponentFromConfigAndChildren(contentElement.Content, contentElement.Content.BuildComponentBuilderConfigurationFromThis());
            TryToAddChildControlToParentControl(parentControl, childControl);
        }
        else if (formElement is ILayoutElement layoutElement)
        {
            foreach (IFormElement child in layoutElement.Children)
            {
                AvaloniaObject childControl = BuildSpecificComponentFromConfigAndChildren(child, child.BuildComponentBuilderConfigurationFromThis());
                TryToAddChildControlToParentControl(parentControl, childControl);
            }
        }
    }
    private void TryToAddChildControlToParentControl(AvaloniaObject parentControl, AvaloniaObject childControl)
    {
        if (parentControl is ContentControl contentControl)
        {
            contentControl.Content = childControl;
        }
        else if (parentControl is Panel layoutControl)
        {
            if(childControl is Control control)
                layoutControl.Children.Add(control);
            else 
                throw new ArgumentException($"Can not add child control of type {childControl.GetType().FullName} to panel: {layoutControl}");
        }
        else
        {
            throw  new ArgumentException($"Cannot add child : {childControl} to {parentControl}, because the parent Control is either not a ContentControl or a Panel!");
        }
    }
    private void AddComponentAttributesToControl(AvaloniaObject control, IComponentBuilderConfiguration config, IFormElement instanceOfRootFormElement)
    {
        foreach (IComponentBuilderAttributeConfiguration attributeConfig in config.ComponentAttributes)
        {
            if (attributeConfig.IsBindingProperty)
            {
                AddBindingAttributeToControl(control, config.ComponentType, attributeConfig);
                if (attributeConfig.AttributeValue != null)
                {
                    AddPrimitiveAttributeToControl(control, config.ComponentType, attributeConfig.AttributeName, attributeConfig.AttributeValue);
                }
            }
            else if (attributeConfig.HasAnotherControlAsValue)
            {
                AvaloniaObject controlAsValue;
                if (attributeConfig.IsAnotherControlAFormElement)
                {
                    controlAsValue = BuildComponent(attributeConfig.FormElementAsValue!); // Checked by the if-clause
                }
                else
                {
                     controlAsValue = BuildSpecificComponentFromConfig(instanceOfRootFormElement, attributeConfig.ControlValueConfig);
                }
                AddPrimitiveAttributeToControl(control, config.ComponentType, attributeConfig.AttributeName, controlAsValue);
            }
            else
            {
                AddPrimitiveAttributeToControl(control, config.ComponentType, attributeConfig.AttributeName, attributeConfig.AttributeValue);
            }
        }
    }
    private void AddBindingAttributeToControl(AvaloniaObject controlInstance, Type typeOfControl, IComponentBuilderAttributeConfiguration config)
    {
        FieldInfo? avaloniaPropertyDescriptorPropertyInfo = typeOfControl.GetField(config.AttributeName);
        if (avaloniaPropertyDescriptorPropertyInfo != null)
        {
            object? avaloniaPropertyDescriptorValue = avaloniaPropertyDescriptorPropertyInfo.GetValue(controlInstance);
            if (avaloniaPropertyDescriptorValue is AvaloniaProperty avaloniaProperty)
            {
                controlInstance.Bind(avaloniaProperty, CreateBindingFromConfig(config));
            }
            else if (avaloniaPropertyDescriptorValue is DirectPropertyBase<object> directProperty)
            {
                controlInstance.Bind(directProperty, CreateBindingFromConfig(config));
            }
            else if (avaloniaPropertyDescriptorValue is StyledProperty<object> styledProperty)
            {
                controlInstance.Bind(styledProperty, CreateBindingFromConfig(config));
            }
        }
    }

    private Binding CreateBindingFromConfig(IComponentBuilderAttributeConfiguration config)
    {
        if (config.TypeOfValueConverter is not null)
            return new Binding(config.NameOfBindingProperty!)
            {
                FallbackValue = config.AttributeValue,
                Converter = new ValueConverterWrapper(config.TypeOfValueConverter)
            };
        return new Binding(config.NameOfBindingProperty!) // Null must be checked above this method.
        {
            FallbackValue = config.AttributeValue,
        };
    }
    private void AddPrimitiveAttributeToControl(AvaloniaObject control, Type typeOfControl, string propertyName, object? propertyValue)
    {
        PropertyInfo? propertyToSet = typeOfControl.GetProperty(propertyName);
        if(propertyToSet == null) throw new ArgumentException($"The property with the given name ({propertyName}) doesn't exist in the given control type ({typeOfControl.FullName})");
        propertyToSet.SetValue(control, propertyValue);
    }

    private class ValueConverterWrapper : IValueConverter
    {

        private MethodInfo? _convertMethod;
        private MethodInfo? _convertBackMethod;
        public ValueConverterWrapper(Type typeOfTheUnderlyingConverter)
        {
            try
            {
                this._convertMethod = typeOfTheUnderlyingConverter.GetMethod("Convert", BindingFlags.Public | BindingFlags.Static);
                this._convertBackMethod = typeOfTheUnderlyingConverter.GetMethod("ConvertBack", BindingFlags.Public | BindingFlags.Static);
                if (this._convertBackMethod is null || this._convertMethod == null)
                    throw new Exception(
                        "Can not extract the methods Convert and ConvertBack from the given Type. Type might not be an IConverter<?,?>. Can not build component.");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Can not extract the methods Convert and ConvertBack from the given Type. Type might not be an IConverter<?,?>. Can not build component. Inner error: " + ex);
            }
        }
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return this._convertMethod.Invoke(null, new object?[]{ value });
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return this._convertMethod.Invoke(null, new object?[] { value });
        }
    }
}