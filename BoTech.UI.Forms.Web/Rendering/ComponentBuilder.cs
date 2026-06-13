using System.Linq.Expressions;
using System.Reflection;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Layout;
using BoTech.UI.Forms.Rendering;
using BoTech.UI.Forms.Web.Html;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.CompilerServices;
using Microsoft.AspNetCore.Components.Rendering;

namespace BoTech.UI.Forms.Web.Rendering;

public class ComponentBuilder : IComponentBuilder<RenderFragment>
{
    public RenderFragment BuildComponent(IFormElement instanceOfRootFormElement)
    {
        return BuildComponentFromConfig(instanceOfRootFormElement.BuildComponentBuilderConfigurationFromThis(), 0, instanceOfRootFormElement);
    }

    private RenderFragment BuildComponentFromConfig(IComponentBuilderConfiguration config, int sequenceCounter, IFormElement instanceOfRootFormElement)
    {
        if(config is not ComponentBuilderConfiguration)
            throw new ArgumentException("The config must be a ComponentBuilderConfiguration!");
        return builder =>
        {
            Console.WriteLine("Staring to build component from config...");
            BuildComponentFromConfigRecursive((ComponentBuilderConfiguration)config, builder, sequenceCounter, instanceOfRootFormElement);
        };
    }

    private void BuildComponentFromConfigRecursive(ComponentBuilderConfiguration config, RenderTreeBuilder builder, int sequenceCounter, IFormElement instanceOfCurrentFormElement)
    {
        if (config.ComponentType != null)
        {
            BuildClassBasedComponentFromConfigRecursive(config, builder, sequenceCounter, instanceOfCurrentFormElement);
        }
        else if (config.HtmlElementComponent != HtmlElements.None)
        {
            BuildHtmlBasedComponentFromConfigRecursive(config, builder, sequenceCounter, instanceOfCurrentFormElement);
        }
        else
            throw new ArgumentException("A component must either be a Html based component or a Blazor class based component!");
    }

    private void BuildHtmlBasedComponentFromConfigRecursive(ComponentBuilderConfiguration config,
        RenderTreeBuilder builder, int sequenceCounter, IFormElement instanceOfCurrentFormElement)
    {
        Console.WriteLine($"Current Html-Element type to build: {config.ComponentType}");
        sequenceCounter += 1;
        builder.OpenElement(sequenceCounter,Enum.GetName(config.HtmlElementComponent).ToLower());
        foreach (ComponentBuilderAttributeConfiguration attributeConfiguration in config.ComponentAttributes)
        {
            Console.WriteLine($"└─>Adding Attribute {attributeConfiguration.AttributeName}");
            builder.AddAttribute(sequenceCounter, attributeConfiguration.AttributeName, attributeConfiguration.AttributeValue);
        }

        BuildChildrenOfComponent(config, builder, instanceOfCurrentFormElement, sequenceCounter);
        builder.CloseElement();
    }
    private void BuildClassBasedComponentFromConfigRecursive(ComponentBuilderConfiguration config,
        RenderTreeBuilder builder, int sequenceCounter, IFormElement instanceOfCurrentFormElement)
    {
        Console.WriteLine($"Current Component type to build: {config.ComponentType}");
        sequenceCounter += 1;
        builder.OpenComponent(sequenceCounter, config.ComponentType);
        foreach (ComponentBuilderAttributeConfiguration attributeConfiguration in config.ComponentAttributes)
        {
            
            if (attributeConfiguration.IsBindingProperty)
            {
                Console.WriteLine($"└─>Adding Attribute {attributeConfiguration.AttributeName}, with binding to Property {attributeConfiguration.AttributeValue}");
                AddAttributeWithBinding(attributeConfiguration, builder, instanceOfCurrentFormElement, sequenceCounter);
            }
            else
            {
                Console.WriteLine($"└─>Adding Attribute {attributeConfiguration.AttributeName}");
                builder.AddAttribute(sequenceCounter, attributeConfiguration.AttributeName, attributeConfiguration.AttributeValue);
            }
        }

        if (config.Children.Count > 0)
        {
            builder.AddAttribute(sequenceCounter + 1, "ChildContent", (RenderFragment)(childBuilder =>
            {
                BuildChildrenOfComponent(config, childBuilder, instanceOfCurrentFormElement, sequenceCounter);
            }));
        }
        builder.CloseComponent();
    }
    private void BuildChildrenOfComponent(ComponentBuilderConfiguration config, RenderTreeBuilder builder, IFormElement instanceOfCurrentFormElement, int sequenceCounter)
    {
        int childSequenceCounter = sequenceCounter + 1;
        IFormElement? currentChild = null;
        if (instanceOfCurrentFormElement is IContentElement contentElement)
            currentChild = contentElement.Content;
        List<IFormElement>.Enumerator? layoutElementChildrenEnumerator = null;
        if (instanceOfCurrentFormElement is ILayoutElement layoutBasedElement)
            layoutElementChildrenEnumerator = layoutBasedElement.Children.GetEnumerator();
        foreach (IComponentBuilderConfiguration childConfig in config.Children)
        {
            if (layoutElementChildrenEnumerator != null && layoutElementChildrenEnumerator.HasValue)
            {
                currentChild = layoutElementChildrenEnumerator.Value.Current;
                layoutElementChildrenEnumerator.Value.MoveNext();
            }
            builder.AddContent(childSequenceCounter, BuildComponentFromConfig(childConfig, childSequenceCounter, currentChild));
        }
    }
    private void AddAttributeWithBinding(ComponentBuilderAttributeConfiguration attribute, RenderTreeBuilder builder, IFormElement instanceOfCurrentFormElement, int sequenceCounter)
    {
        Console.WriteLine($"└──>trying to find property with name {attribute.NameOfBindingProperty} in the class {attribute.TypeWhereTheBindingPropertyIsDeclared} with obj: {instanceOfCurrentFormElement}");
        // Get the field/property via reflection
        FieldInfo? field = attribute.TypeWhereTheBindingPropertyIsDeclared.GetField(attribute.NameOfBindingProperty, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        PropertyInfo? propertyInfoOfPropertyInClass  = attribute.TypeWhereTheBindingPropertyIsDeclared.GetProperty(attribute.NameOfBindingProperty, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        Type valueType;
        if (field != null)
        {
            valueType = field.FieldType;
            Console.WriteLine($"└───>Property is: {field.ToString()}");
        }
        else if (propertyInfoOfPropertyInClass != null)
        {
            valueType = propertyInfoOfPropertyInClass.GetType();
            Console.WriteLine($"└───>Property is: {propertyInfoOfPropertyInClass.ToString()}");
        }
        else
        {
            throw new Exception("Given name is not a field or property");
        }
        
        object? Getter()
        {
            if (field != null)
                return field.GetValue(instanceOfCurrentFormElement);
            else if(propertyInfoOfPropertyInClass != null)
                return propertyInfoOfPropertyInClass.GetValue(instanceOfCurrentFormElement);
            return null;
        }
        void Setter(object value)
        {
            if (field != null) 
                field.SetValue(instanceOfCurrentFormElement, value);
            else if(propertyInfoOfPropertyInClass != null)
                propertyInfoOfPropertyInClass.SetValue(instanceOfCurrentFormElement, value);
        }
        Console.WriteLine("└──>Adding Getter");
        // 1. Value
        builder.AddAttribute(sequenceCounter, attribute.AttributeName, Getter());
        Console.WriteLine("└──>Adding Setter");
        // 2. ValueChanged
        builder.AddAttribute(sequenceCounter, attribute.AttributeName + "Changed",
            EventCallback.Factory.Create(this, (Action<object>)(value => Setter(value))));
        Console.WriteLine("└──>Adding Expression");
        // 3. ValueExpression
        builder.AddAttribute(sequenceCounter, attribute.AttributeName + "Expression",
            RuntimeHelpers.TypeCheck(
                (Expression)(Expression.Lambda(
                    Expression.Convert(
                        Expression.PropertyOrField(Expression.Constant(this), attribute.NameOfBindingProperty),
                        valueType)))));
    }


}