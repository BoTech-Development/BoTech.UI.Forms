namespace BoTech.UI.Forms.Controls.Layout;

public class AStack : ILayoutElement
{
    public required List<IFormElement> Children { get; init; }
    public Orientation Orientation { get; init; } = Orientation.Vertical;
    public void Show()
    {
        throw new NotImplementedException();
    }

    
}