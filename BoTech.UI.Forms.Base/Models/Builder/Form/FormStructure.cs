using Avalonia.Controls;
using Avalonia.Styling;
using BoTech.UI.Controls;
using BoTech.UI.Controls.Forms;


namespace BoTech.UI.Forms.Base.Models.Builder.Form;
/// <summary>
/// This Model represents the FormControl. It saves all FormInput and Groups of the Form.
/// </summary>
public class FormStructure
{
    public NestedFormViewModelDeclaration ReferencedViewModelDeclaration { get; set; }
    /// <summary>
    /// All Inputs for this part of the Form.
    /// They will be safed separately to make it easier to access the <see cref="FormInput.GetResult">GetResult</see> Methods.
    /// </summary>
    public List<FormInput> FormInputs { get; set; } = new List<FormInput>();
    /// <summary>
    /// The Name of the Form. This Text will be placed above this Group.
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// The FormInput Controls which are Aligned in a StackPanel.
    /// Might be null but <see cref="GridAlignedInputs"/> must be not null.
    /// </summary>
    public StackPanel? StackAlignedInputs { get; set; } = null;
    /// <summary>
    /// The FormInput Controls which are Aligned in a Grid.
    /// Might be null but <see cref="StackAlignedInputs"/> must be not null.
    /// </summary>
    public Grid? GridAlignedInputs { get; set; } = null;
    /// <summary>
    /// Only needed to execute <see cref="UpdateGroupBox"/>
    /// </summary>
    private GroupBox _groupBox;
    /// <summary>
    /// The Stack or the Grid are added to the GroupBox from the FormBuilder.
    /// </summary>
    public GroupBox GroupBox
    {
        get
        {
            UpdateGroupBox();
            return _groupBox;
        }
        private set
        {
            _groupBox = value;
        }
       
    }

    /// <summary>
    /// All other Groups which a sub groups.
    /// </summary>
    public List<FormStructure> SubGroups { get; set; } = new List<FormStructure>();

    private void UpdateGroupBox()
    {
        if (StackAlignedInputs != null)
        {
            GroupBox = new GroupBox()
            {
                Header = Title,
                Content = StackAlignedInputs
            };
            return;
        }else if (GridAlignedInputs != null)
        {
            GroupBox = new GroupBox()
            {
                Header = Title,
                Content = GridAlignedInputs
            };
            return;
        }
        throw new InvalidOperationException("FormStructure has not been initialized! Initialize FormStructure first before accessing the GroupBox Property! (" + this.ToString()+ ")");
    }
}