namespace BoTech.UI.Forms.Converter;

public interface IConverter<TSource, TDestination> where TSource : class where TDestination : class
{
    public static TDestination Convert(TSource source){throw new NotImplementedException();}
}