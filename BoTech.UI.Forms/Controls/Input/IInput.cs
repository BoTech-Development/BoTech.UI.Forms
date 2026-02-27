namespace BoTech.UI.Forms.Controls.Input;

public interface IInput<T> : IFormElement
{
    public string Description { get; init; }
    public string Name { get; init; } 
    /// <summary>
    /// The Property in the Viewmodel this Input is bound to.
    /// </summary>
    public string Property { get; init; }
    /// <summary>
    /// The current value of the visual element
    /// </summary>
    T Value { get; }
    /// <summary>
    /// Will be called by the backend when the user edits the Element
    /// </summary>
    public void OnUserUpdatedValue();

}