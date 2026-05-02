using BoTech.UI.Forms.Controls.Layout;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Controls.Input.DateTime;

public interface ITimeInput : IInput<TimeOnly>
{
    public void SetToCurrentTime();
}