using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Controls.Layout.Grid;

public interface IGrid : IFormElement
{
    public List<IRow> Rows { get; set; }
}