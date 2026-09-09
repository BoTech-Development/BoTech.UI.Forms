namespace BoTech.UI.Forms.Rendering.Helper;

public interface IComponentBuilderConfigurationDeserializer<T> where T : IComponentBuilderConfiguration
{
    /// <summary>
    /// Deserializes an xml string to an implementation of IComponentBuilderConfiguration
    /// </summary>
    /// <param name="xml">The xml to deserialize</param>
    /// <returns>the new instance.</returns>
    public static abstract T DeserializeFromString(string xml);
    /// <summary>
    /// Loads the given resource:
    ///     Resources should have the following name "<DefaultName>.<Folder>.<FileName>"
    /// and parses the xml string to a IComponentBuilderConfiguration implementation
    /// </summary>
    /// <param name="fullResourceName">The full name of the resource where the xml string is located</param>
    /// <returns>the new instance.</returns>
    public static abstract T DeserializeFromResourceFile(string fullResourceName);
}