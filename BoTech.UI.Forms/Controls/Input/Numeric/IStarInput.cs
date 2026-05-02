namespace BoTech.UI.Forms.Controls.Input.Numeric;

public interface IStarInput : INumberInput<int>
{
    public int NumberOfStars { get; init; }
}