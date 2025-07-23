namespace BoTech.UI.Forms.Base.Models.PropertyAnnotations;

/// <summary>
/// This Annotation can be used to position an input element in a Grid.
/// </summary>
[AttributeUsage(AttributeTargets.Property )]
public class FormInputPropertyGridItem : FormInputProperty
{
    public int ColumnIndex { get; set; }
    public int RowIndex { get; set; }
    public FormInputPropertyGridItem(string name, string description, object? defaultValue, int rowIndex, int columnIndex) : base(name, description, defaultValue)
    {
        ColumnIndex = columnIndex;
        RowIndex = rowIndex;
    }
}