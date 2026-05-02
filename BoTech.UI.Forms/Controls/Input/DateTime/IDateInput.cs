using BoTech.UI.Forms.Controls.Layout;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Controls.Input.DateTime;

public interface IDateInput : IInput<DateOnly>
{
    public void SetToCurrentDate();
}