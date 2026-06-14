using System;
using System.Diagnostics;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Layout;
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
        if (formElement is IContentElement contentElement)
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
        foreach (ComponentBuilderAttributeConfiguration attributeConfig in config.ComponentAttributes)
        {
            if (attributeConfig.IsBindingProperty)
            {
                AddBindingAttributeToControl(control, config.ComponentType, attributeConfig);
            }
            else if (attributeConfig.HasAnotherControlAsValue)
            {
                AvaloniaObject controlAsValue = BuildSpecificComponentFromConfig(instanceOfRootFormElement, attributeConfig.ControlValueConfig);
                AddPrimitiveAttributeToControl(control, config.ComponentType, attributeConfig.AttributeName, controlAsValue);
              /*  if (attributeConfig.ShouldStoreAnotherControlAsValueResult)
                    StoreNewControlInFormElement(attributeConfig.NameOfPropertyToStoreTheResultValue, config.ConfigurationForFormElement, controlAsValue);*/
            }
            else
            {
                AddPrimitiveAttributeToControl(control, config.ComponentType, attributeConfig.AttributeName, attributeConfig.AttributeValue);
            }
        }
    }

 /*   private void StoreNewControlInFormElement(string propertyToStoreThNewInstanceIn, IFormElement formElement,
        AvaloniaObject controlToStore)
    {
        PropertyInfo? propertyToSet = formElement.GetType().GetProperty(propertyToStoreThNewInstanceIn);
        FieldInfo? fieldInfo = formElement.GetType().GetField(propertyToStoreThNewInstanceIn);
        if (propertyToSet == null && fieldInfo == null)
            throw new ArgumentException($"Property {propertyToStoreThNewInstanceIn} does not exist in the form element {formElement}, where the control {controlToStore} should be stored.");
        if(propertyToSet != null)
            propertyToSet.SetValue(formElement, controlToStore);
        if(fieldInfo != null)
            fieldInfo.SetValue(formElement, controlToStore);
    }*/
    private void AddBindingAttributeToControl(AvaloniaObject controlInstance, Type typeOfControl, ComponentBuilderAttributeConfiguration config)
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

    private Binding CreateBindingFromConfig(ComponentBuilderAttributeConfiguration config)
    {
        return new Binding(config.NameOfBindingProperty!) // Null must be checked above this method.
        {
            FallbackValue = config.AttributeValue
        };
    }
    private void AddPrimitiveAttributeToControl(AvaloniaObject control, Type typeOfControl, string propertyName, object? propertyValue)
    {
        PropertyInfo? propertyToSet = typeOfControl.GetProperty(propertyName);
        if(propertyToSet == null) throw new ArgumentException($"The property with the given name ({propertyName}) doesn't exist in the given control type ({typeOfControl.FullName})");
        propertyToSet.SetValue(control, propertyValue);
    }
    
}