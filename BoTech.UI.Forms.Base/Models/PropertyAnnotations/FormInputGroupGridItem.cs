namespace BoTech.UI.Forms.Base.Models.PropertyAnnotations;
/// <summary>
/// This Annotation can be used to position a subgroup in a Grid.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class FormInputGroupGridItem : FormInputGroup
{
    public int ColumnIndex { get; set; }
    public int RowIndex { get; set; }
    public FormInputGroupGridItem(string groupName, string groupLabel, int rowIndex, int columnIndex) : base(groupName, groupLabel)
    {
        ColumnIndex = columnIndex;
        RowIndex = rowIndex;
    }
}