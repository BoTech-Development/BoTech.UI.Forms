namespace BoTech.UI.Forms.Controls.Input.Numeric;
/// <summary>
/// A star input is a set of starts, where the user can select a specific star value
/// The max number of stars is 255
/// </summary>
public interface IStarInput : INumberInput<byte>
{
    /// <summary>
    /// Here this value represents the maximal number of stars which can be selected.
    /// </summary>
   // byte INumberInput<byte>.Maximum { get; set; }
    /// <summary>
    /// The count of stars, which the user has checked (by clicking each star will be marked)
    /// </summary>
    public int NumberOfStarsThatAreChecked { get; init; }
    /// <summary>
    /// If true, the last star is not fully marked, but half.
    /// </summary>
    public bool IsHalfOfLastStarChecked { get; init; }
    /// <summary>
    /// The count of stars, which the user has not checked (by clicking each star will be marked and clicking on it again the star will be unchecked)
    /// </summary>
    public int NumberOfStarsThatAreUnchecked { get; init; }
}