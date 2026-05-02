using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Controls.Layout.Grid;

public interface IRow : IFormElement
{
    public List<IColumn> Columns { get; set; }
}