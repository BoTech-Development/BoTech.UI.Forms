namespace BoTech.UI.Forms.Base.Models.PropertyAnnotations;
/// <summary>
/// Defines a group which will be displayed with a Grid
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class,  AllowMultiple = false)]
public class FormInputGroupGrid : FormInputGroup
{
    public int CountOfRows { get;}
    public int CountOfColumns { get;}
    public FormInputGroupGrid(string groupName, string groupLabel, int countOfRows, int countOfColumns) : base(groupName, groupLabel)
    {
        CountOfRows = countOfRows;
        CountOfColumns = countOfColumns;
    }
}