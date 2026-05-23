using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Controls.Input.Text;

public interface ITextInput : IInput<string>
{
    public bool IsMultiline { get; init; }
}