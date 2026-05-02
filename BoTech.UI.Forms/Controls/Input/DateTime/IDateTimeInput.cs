using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Controls.Input.DateTime;

public interface IDateTimeInput : IInput<System.DateTime>
{
    public void SetToCurrentDateTime();
}