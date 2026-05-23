namespace BoTech.UI.Forms.Controls.Input.Numeric;

public interface IStarInput : INumberInput<int>
{
    /// <summary>
    /// Defines how many stars should be displayed.
    /// The Maximum is 255.
    /// </summary>
    public byte NumberOfStars { get; init; }
}