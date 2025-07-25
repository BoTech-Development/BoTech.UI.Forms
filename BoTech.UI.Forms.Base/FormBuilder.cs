using System.Reflection;
using BoTech.UI.Forms.Base.Controls;
using BoTech.UI.Forms.Base.Models;
using BoTech.UI.Forms.Base.Models.Builder;
using BoTech.UI.Forms.Base.Models.PropertyAnnotations;

namespace BoTech.UI.Forms.Base;

public class FormBuilder
{
    public static object? groupBoxClassic = null;
    public static void Init(bool found, object? resource)
    {
        if (found)
        {
            groupBoxClassic = resource;
        }
    }
    public FormControl CreateFormFor(FormViewModelBase model)
    {
        NestedFormViewModelDeclaration mainGroup = GetAllNestedFormViewModelsFormOneType(model);
        return FormControl.Create(mainGroup);
    }

    private NestedFormViewModelDeclaration GetAllNestedFormViewModelsFormOneType(FormViewModelBase model) => GetAllNestedFormViewModelsFormOneType(model.GetType());
    /// <summary>
    /// Extracts all necessary information to create a NestedFormViewModelDeclaration from a type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private NestedFormViewModelDeclaration GetAllNestedFormViewModelsFormOneType(Type type)
    {
        NestedFormViewModelDeclaration nestedFormViewModelDeclaration = new NestedFormViewModelDeclaration()
        {
            ViewModelType = type,
        };
        AddGroupInformation(nestedFormViewModelDeclaration, type);
        AddFormResultProperties(nestedFormViewModelDeclaration, type);
        AddDeclaredFormProperties(nestedFormViewModelDeclaration, type);
        // Make a recursion step
        List<Type> nestedClasses = type.GetNestedTypes().ToList();
        foreach (Type nestedClass in nestedClasses)
        {
            nestedFormViewModelDeclaration.SubViewModels.Add(GetAllNestedFormViewModelsFormOneType(nestedClass));
        }
        return nestedFormViewModelDeclaration;
    }

    private void AddFormResultProperties(NestedFormViewModelDeclaration nestedFormViewModelDeclaration, Type type)
    {
        foreach (PropertyInfo property in type.GetProperties())
        {
            List<object> annotations = property.GetCustomAttributes(typeof(FormResultProperty), false).ToList();
            if (annotations.Count == 1)
            {
                nestedFormViewModelDeclaration.FormResultProperties.Add(property);
            }
        }
    }
    /// <summary>
    /// Adda all properties that have a PropertyInfor Annotation to the List declared Properties
    /// </summary>
    /// <param name="nestedFormViewModelDeclaration"></param>
    /// <param name="type"></param>
    private void AddDeclaredFormProperties(NestedFormViewModelDeclaration nestedFormViewModelDeclaration, Type type)
    {
        foreach (PropertyInfo property in type.GetProperties())
        {
            List<object> annotations = property.GetCustomAttributes(typeof(FormInputProperty), false).ToList();
            if (annotations.Count == 1)
            {
                nestedFormViewModelDeclaration.DeclaredFormProperties.Add(new FormProperty(property, (FormInputProperty)annotations[0]));
            }
        }
    }
    private void AddGroupInformation(NestedFormViewModelDeclaration nestedFormViewModelDeclaration, Type type)
    {
        List<object> annotations = type.GetCustomAttributes(typeof(FormInputGroup), false).ToList();
        if (annotations.Count == 1)
        {
            nestedFormViewModelDeclaration.GroupInformation = (FormInputGroup)annotations[0];
        }
    }
    
}