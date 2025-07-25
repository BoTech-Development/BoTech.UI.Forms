using System.Reflection;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using BoTech.UI.Controls;
using BoTech.UI.Controls.Forms;
using BoTech.UI.Forms.Models;
using BoTech.UI.Forms.Models.Builder;
using BoTech.UI.Forms.Models.Builder.Form;
using BoTech.UI.Forms.Models.PropertyAnnotations;
using Material.Icons;
using Material.Icons.Avalonia;

namespace BoTech.UI.Forms.Controls;

public delegate void OnFormAccepted(FormViewModelBase result);
public delegate void OnFormCancelled(FormViewModelBase currentStatus);
public class FormControl : ContentControl
{
    public event OnFormAccepted OnFormAccepted;
    public event OnFormCancelled OnFormCancelled;
    public FormStructure ParentFormStructure { get; private set; }
    private FormControl(Control content,  FormStructure parentFormStructure)
    {
        ParentFormStructure = parentFormStructure;
        Content = new GroupBox()
        {
            Header = ParentFormStructure.Title,
            Content = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                Children =
                {
                    content, 
                    new StackPanel()
                    {
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Orientation = Orientation.Horizontal,
                        Children =
                        {
                            CreateCancelButton(),
                            CreatSubmitButton()
                        }
                    }
                }
            },
        };
    }

    private Button CreateCancelButton()
    {
        Button cancelButton = new Button()
        {
            Content = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {

                    new MaterialIcon()
                    {
                        Kind = MaterialIconKind.CloseCircle
                    },
                    new TextBlock()
                    {
                        Text = "Cancel"
                    },
                }
            }
        };
        cancelButton.Click += OnCancelButtonClick;
        return cancelButton;
    }

    private void OnCancelButtonClick(object? sender, RoutedEventArgs e)
    {
        OnFormCancelled.Invoke(CreateViewModelFromInput(ParentFormStructure, null));
    }

    private Button CreatSubmitButton()
    {
        Button submitButton = new Button()
        {
            Content = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {

                    new MaterialIcon()
                    {
                        Kind = MaterialIconKind.CheckboxMarkedCircle
                    },
                    new TextBlock()
                    {
                        Text = "Submit"
                    },
                }
            },
        };
        submitButton.Click += OnSubmitButtonClick;
        return submitButton;
    }

    private void OnSubmitButtonClick(object? sender, RoutedEventArgs e)
    {
        OnFormAccepted.Invoke(CreateViewModelFromInput(ParentFormStructure, null));
    }
    /// <summary>
    /// Creates all ViewModels for each FormStruture and injects all Results of the InputControls into the ViewModel.
    /// </summary>
    /// <param name="formStructure"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    private FormViewModelBase CreateViewModelFromInput(FormStructure formStructure, FormStructure? parent)
    {   
        FormViewModelBase result = CreateViewModelAndPopulate(formStructure);
        // When parent is null, this formStructure is already the parent
        if(parent != null) InjectViewModelInstanceIntoResultProperty(result, formStructure, parent);
        foreach (FormStructure subGroup in formStructure.SubGroups)
        {
            CreateViewModelFromInput(subGroup, formStructure);
        }
        return result;
    }
    /// <summary>
    /// Saves the current ViewModel (A nested class in the parent ViewModel) in the FormResultProperty marked Property.
    /// </summary>
    /// <param name="viewModel">The sub ViewModel</param>
    /// <param name="formStructure">The nested class</param>
    /// <param name="parent">The parent model.</param>
    private void InjectViewModelInstanceIntoResultProperty(FormViewModelBase viewModel, FormStructure formStructure, FormStructure parent)
    {
        foreach (PropertyInfo formResultProperty in parent.ReferencedViewModelDeclaration.FormResultProperties)
        {
            if (formResultProperty.PropertyType == formStructure.ReferencedViewModelDeclaration.ViewModelType)
            {
                formResultProperty.SetValue(ParentFormStructure.Instance, viewModel);
                return;
            }
        }
    }
    /// <summary>
    /// Creates an Instance of the ViewModel for the FormStructure and populates all Properties with the Result Values of the Input Controls.
    /// </summary>
    /// <param name="formStructure"></param>
    /// <returns>The created FormViewModel.</returns>
    private FormViewModelBase CreateViewModelAndPopulate(FormStructure formStructure)
    {
        FormViewModelBase viewModel = (FormViewModelBase)Activator.CreateInstance(formStructure.ReferencedViewModelDeclaration.ViewModelType);
        foreach (FormProperty formProperty in  formStructure.ReferencedViewModelDeclaration.DeclaredFormProperties)
        {
            if (formStructure.FormInputs.ContainsKey(formProperty))
            {
                FormInput inputControl = formStructure.FormInputs[formProperty];
                object? value = Convert.ChangeType(inputControl.Result, formProperty.Property.PropertyType);
                formProperty.Property.SetValue(viewModel, value);
            }
        }
        formStructure.Instance = viewModel;
        return viewModel;
    }

    /// <summary>
    /// This Method uses the extracted Class structure to convert it into a visible Control.
    /// </summary>
    /// <param name="nestedFormViewModelDeclaration">The extracted Class structure</param>
    /// <returns>The visible Control</returns>
    public static FormControl Create(NestedFormViewModelDeclaration nestedFormViewModelDeclaration)
    {
        FormStructure parentFormStructure = CreateFormStructureRecursive(nestedFormViewModelDeclaration, null);
        if(parentFormStructure.StackAlignedInputs != null) return new FormControl(parentFormStructure.StackAlignedInputs, parentFormStructure);
        if (parentFormStructure.GridAlignedInputs != null) return new FormControl(parentFormStructure.GridAlignedInputs, parentFormStructure);
        throw new Exception("FormControl.Create(): Error by creating the Content value.");
    }

    /// <summary>
    /// Converts all NestedFormViewModels into FormStructures.
    /// </summary>
    /// <param name="nestedFormViewModelDeclaration"></param>
    /// <param name="parent">Only for recursion</param>
    private static FormStructure CreateFormStructureRecursive(NestedFormViewModelDeclaration nestedFormViewModelDeclaration,
        FormStructure? parent)
    {
        FormStructure structure = new FormStructure();
        structure.ReferencedViewModelDeclaration = nestedFormViewModelDeclaration;
        bool isStackPanel = InitializeFormStructure(nestedFormViewModelDeclaration, structure);
        CreateFormInputs(nestedFormViewModelDeclaration, structure, isStackPanel);
        // Recursion
        foreach (NestedFormViewModelDeclaration subGroup in nestedFormViewModelDeclaration.SubViewModels)
        {
            CreateFormStructureRecursive(subGroup, structure);
        }

        // Add the new structure to the Parent structure
        if (parent != null)
        {
            parent.SubGroups.Add(structure);
            AddSubGroupControlsToParent(parent, structure, nestedFormViewModelDeclaration);
        }
        return structure;
    }
    /// <summary>
    /// Put the new controls which are placed in a stack or grid in the stack or grid of the parent group.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="subGroup"></param>
    private static void AddSubGroupControlsToParent(FormStructure parent, FormStructure subGroup, NestedFormViewModelDeclaration subGroupClassStructure)
    {
        // Check if the parent group either uses a grid or a stack for the layout.
        if (parent.GridAlignedInputs != null)
        {
            // the sub class must have the FormInputGroupGridItem defined.
            if (subGroupClassStructure.GroupInformation is FormInputGroupGridItem gridItem)
            {
              /*  if (subGroup.GridAlignedInputs != null)
                {
                    Grid.SetRow(subGroup.GridAlignedInputs, gridItem.RowIndex);
                    Grid.SetColumn(subGroup.GridAlignedInputs, gridItem.ColumnIndex);
                    parent.GridAlignedInputs.Children.Add(subGroup.GridAlignedInputs);
                }
                else if (subGroup.StackAlignedInputs != null)
                {
                    Grid.SetRow(subGroup.StackAlignedInputs, gridItem.RowIndex);
                    Grid.SetColumn(subGroup.StackAlignedInputs, gridItem.ColumnIndex);
                    parent.GridAlignedInputs.Children.Add(subGroup.StackAlignedInputs);
                }*/
                GroupBox box = subGroup.GroupBox;
                Grid.SetRow(box, gridItem.RowIndex);
                Grid.SetColumn(box, gridItem.ColumnIndex);
                parent.GridAlignedInputs.Children.Add(box);
            }
            else
            {
                throw new ArgumentException("You must define the FormInputGroup Annotation in Type: " +
                                            subGroupClassStructure.ViewModelType);
            }
        }
        else if (parent.StackAlignedInputs != null)
        {
            parent.StackAlignedInputs.Children.Add(subGroup.GroupBox);
            
         /*   if (subGroup.StackAlignedInputs != null)
            {
                parent.StackAlignedInputs.Children.Add(subGroup.StackAlignedInputs);
            }else if (subGroup.GridAlignedInputs != null)
            {
                parent.StackAlignedInputs.Children.Add(subGroup.GridAlignedInputs);
            }*/
        }
        else
        {
            throw new ArgumentException("You must use either FormInputGroupStack or FormInputGroupGrid in the parent class.");
        }
    }
    /// <summary>
    /// Converts all FormProperties into a visible and editable Control.
    /// </summary>
    /// <param name="nestedFormViewModelDeclaration"></param>
    /// <param name="structure"></param>
    /// <param name="isStackPanel"></param>
    private static void CreateFormInputs(NestedFormViewModelDeclaration nestedFormViewModelDeclaration, FormStructure structure, bool isStackPanel)
    {
        structure.Title = nestedFormViewModelDeclaration.GroupInformation.GroupLabel;
        foreach (FormProperty formProperty in nestedFormViewModelDeclaration.DeclaredFormProperties)
        {
            FormInput input = CreateInputElementFromFormProperty(formProperty, formProperty.Annotation.DefaultValue);
            structure.FormInputs.Add(formProperty, input);
            if (isStackPanel)
            {
                // Is never null because of InitializeFormStructure method
                structure.StackAlignedInputs!.Children.Add(input);
            }
            else
            {
                if (formProperty.Annotation is FormInputPropertyGridItem gridItem)
                {
                    // Is never null because of InitializeFormStructure method
                    Grid.SetRow(input, gridItem.RowIndex);
                    Grid.SetColumn(input, gridItem.ColumnIndex);
                    structure.GridAlignedInputs!.Children.Add(input);
                }
            }
        }
    }
    /// <summary>
    /// Initialize the Grid or the StackPanel that the NestedFormViewModelDeclaration defines.
    /// </summary>
    /// <param name="nestedFormViewModelDeclaration"></param>
    /// <param name="structure"></param>
    /// <returns>True when the StackPanel were initialized or false when the Grid was initialized.</returns>
    /// <exception cref="ArgumentException">Occurs when the user does not define either a StackPanel or a Grid. </exception>
    private static bool InitializeFormStructure(NestedFormViewModelDeclaration nestedFormViewModelDeclaration, FormStructure structure)
    {
        if (nestedFormViewModelDeclaration.GroupInformation is FormInputGroupGrid grid)
        {
            ColumnDefinitions columnDefinitions = new ColumnDefinitions();
            for(int c = 0; c < grid.CountOfColumns; c++) columnDefinitions.Add(new ColumnDefinition(GridLength.Auto));
            RowDefinitions rowDefinitions = new RowDefinitions();
            for(int r = 0; r < grid.CountOfRows; r++) rowDefinitions.Add(new RowDefinition(GridLength.Auto));
            structure.GridAlignedInputs = new Grid()
            {
                ColumnDefinitions = columnDefinitions,
                RowDefinitions = rowDefinitions,
            };
            return false;
        }
        else if(nestedFormViewModelDeclaration.GroupInformation is FormInputGroupStack stack)
        {
            structure.StackAlignedInputs = new StackPanel()
            {
                Orientation = stack.StackOrientation
            };
            return true;
        }
        else
        {
            throw new ArgumentException("You must use either FormInputGroupStack or FormInputGroupGrid in the parent class.");
        }    
    }
    /// <summary>
    /// This method creates an input element depend on the type of the property.
    /// </summary>
    /// <param name="property"></param>
    /// <param name="defaultValue"></param>
    /// <returns>An displayable input element.</returns>
    /// <exception cref="ArgumentException">When you enter invalid types in the defaultValue property, that does not match the given property Type.</exception>
    /// <exception cref="NotSupportedException">Some Types are not supported yet.</exception>
    private static FormInput CreateInputElementFromFormProperty(FormProperty property, object? defaultValue)
    {
        Type propertyType = property.Property.PropertyType;
        if (propertyType == typeof(bool))
        {
            if(defaultValue is not bool) throw new ArgumentException("The type of " + nameof(defaultValue) + "is invalid you must invoke this method with an boolean or bool.");
            return new BoolFormInput(property.Annotation.Name, property.Annotation.HelpText, (bool)defaultValue);
        }
        else if (propertyType == typeof(byte))
        { 
           // if(defaultValue is not bool) throw new ArgumentNullException("The type of " + nameof(defaultValue) + "is invalid you must invoke this method with an byte.");
            throw new NotSupportedException("The type of byte is not supported in v1.0.1");
        }
        else if (propertyType == typeof(char))
        {
            throw new NotSupportedException("The type of char is not supported in v1.0.1");
        }
        else if (propertyType == typeof(string))
        {
            if(defaultValue is not string) throw new ArgumentException("The type of " + nameof(defaultValue) + "is invalid you must invoke this method with an string.");
            return new TextFormInput(property.Annotation.Name, property.Annotation.HelpText, (string)defaultValue);
        }
        else if (propertyType == typeof(int))
        {
            return CreateNumericInput(property, defaultValue);
        }
        else if (propertyType == typeof(double))
        {
            return CreateNumericInput(property, defaultValue);
        }
        else if (propertyType == typeof(float))
        {
            return CreateNumericInput(property, defaultValue);
        }
        else if (propertyType == typeof(object))
        {
            
        }
        return null;
    }

    private static IFormInput CreateFormInputForObject(FormProperty property)
    {
        return null;
    }
    /// <summary>
    /// General Method that creates an numeric input for a specific property in the ViewModel.
    /// </summary>
    /// <param name="property"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static FormInput CreateNumericInput(FormProperty property, object? defaultValue)
    {
        if (property.Annotation is NumericFormInputProperty numericFormInputProperty)
        {
            return new NumberFormInput(property.Annotation.Name, property.Annotation.HelpText, numericFormInputProperty.Configuration);
        }
        else // Just use the default params
        {

            NumberFormInput.NumericUpDownConfiguration configuration = new NumberFormInput.NumericUpDownConfiguration()
            {
                Increment = 1,
            };
            try
            {
                configuration.Value = (decimal)defaultValue;
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    "Can not create an Numeric form input without an float, int or double input!!!");
            }
            return new NumberFormInput(property.Annotation.Name, property.Annotation.HelpText, new NumberFormInput.NumericUpDownConfiguration()
            {
                Value = (int)defaultValue,
            });
        }
    }
}