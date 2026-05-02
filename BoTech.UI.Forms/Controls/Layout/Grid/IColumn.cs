using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Controls.Layout.Grid;

public interface IColumn : IContentElement
{
    public IFormElement Content { get; set; }
    
}