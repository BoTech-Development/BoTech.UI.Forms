namespace BoTech.UI.Forms.Rendering.Helper;

public interface IComponentBuilderConfigurationDeserializer<T> where T : IComponentBuilderConfiguration
{
    /// <summary>
    /// Deserializes a json string to an implementation of IComponentBuilderConfiguration
    /// </summary>
    /// <param name="json">The json to deserialize</param>
    /// <returns>the new instance.</returns>
    public static abstract T DeserializeFromString(string json);
    /// <summary>
    /// Loads the given resource:
    ///     Resources should have the following name "<DefaultName>.<Folder>.<FileName>"
    /// and parses the json string to a IComponentBuilderConfiguration implementation
    /// </summary>
    /// <param name="fullResourceName">The full name of the resource where the json string is located</param>
    /// <returns>the new instance.</returns>
    public static abstract T DeserializeFromResourceFile(string fullResourceName);
}