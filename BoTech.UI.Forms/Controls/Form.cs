
namespace BoTech.UI.Forms.Controls;

public class Form
{
    public List<IFormElement> Content { get; init; } = new List<IFormElement>();
    public bool IsStepped { get; init; }
}