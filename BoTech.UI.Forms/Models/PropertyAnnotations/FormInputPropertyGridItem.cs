namespace BoTech.UI.Forms.Models.PropertyAnnotations;

/// <summary>
/// This Annotation can be used to position an input element in a Grid.
/// </summary>
[AttributeUsage(AttributeTargets.Property )]
public class FormInputPropertyGridItem : FormInputProperty
{
    public int ColumnIndex { get; set; }
    public int RowIndex { get; set; }
    public FormInputPropertyGridItem(string name, string description, bool isRequired, object? defaultValue, int rowIndex, int columnIndex) : base(name, description, isRequired, defaultValue)
    {
        ColumnIndex = columnIndex;
        RowIndex = rowIndex;
    }
}