namespace BoTech.UI.Forms.Converter;
/// <summary>
/// This class represents a standard object type converter.
/// This class can convert the instance of one specific object type to a destination object type.
/// </summary>
/// <typeparam name="TSource">The type of the object which should be converted</typeparam>
/// <typeparam name="TDestination">The type of the object after the conversion.</typeparam>
public interface IConverter<TSource, TDestination>
{
    /// <summary>
    /// Converts the instance of an object to a specific type.
    /// </summary>
    /// <param name="source">the instance to convert</param>
    /// <returns>The converted instance which has the destination type.</returns>
    public static abstract TDestination Convert(TSource source);
    /// <summary>
    /// Converts the instance back to the original object.
    /// This method must not be implemented, when not necessary.
    /// </summary>
    /// <param name="source">The object to convert back</param>
    /// <returns>The original object (not ref equals)</returns>
    public static abstract TSource ConvertBack(TDestination source);
}