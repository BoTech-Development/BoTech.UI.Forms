namespace BoTech.UI.Forms.Controls.Input;

public interface IInput<T> : IFormElement, IDescribable, INameable
{
    /// <summary>
    /// The Property in the Viewmodel this Input is bound to.
    /// </summary>
    public string Property { get; init; }
    /// <summary>
    /// The current value of the visual element
    /// </summary>
    T Value { get; }
    /// <summary>
    /// Will be called by the backend when the user finished editing this Element.
    /// Classes that implement this interface must invoke this event.
    /// </summary>
    public event EventHandler OnUserUpdatedValue;

}