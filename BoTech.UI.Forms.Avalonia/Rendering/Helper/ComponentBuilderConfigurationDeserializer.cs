using System.Reflection;
using System.Text.Json;
using BoTech.UI.Forms.Rendering.Helper;
using BoTech.XmlParser;

namespace BoTech.UI.Forms.Avalonia.Rendering.Helper;

public class ComponentBuilderConfigurationDeserializer : IComponentBuilderConfigurationDeserializer<ComponentBuilderConfiguration>
{
    public static ComponentBuilderConfiguration DeserializeFromString(string xml)
    {
        ComponentBuilderConfiguration? result = (ComponentBuilderConfiguration)new XmlDeserializer().Deserialize(xml);
        if (result == null) 
            throw new InvalidOperationException($"Could not deserialize '{xml}' to build component builder configuration.");
        return result;
    }

    public static ComponentBuilderConfiguration DeserializeFromResourceFile(string fullResourceName)
    {
        using Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullResourceName);
        if (stream == null)
            throw new FileNotFoundException($"Embedded resource '{fullResourceName}' not found in assembly '{Assembly.GetExecutingAssembly().FullName}'.");

        using var reader = new StreamReader(stream);
        string jsonContent = reader.ReadToEnd();
        return DeserializeFromString(jsonContent);
    }
}