using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using BoTech.UI.Forms.Controls;

namespace BoTech.UI.Forms.Analyzer;

public class BoFormParser
{
    public BoForm? ParseAndValidateBoFormFile(string xmlFileName, Assembly? assembly = null)
    {
        if (File.Exists(xmlFileName))
        {
            if (assembly == null) assembly = Assembly.GetCallingAssembly();
            return TryToParseAndValidateBoForm(File.OpenRead(xmlFileName), assembly);
        }
        return null;
    }
    /// <summary>
    /// Load the BoFrom from an internal Project Resource 
    /// </summary>
    /// <param name="resourceFileNameAndNamespace">The exact Namespace of the File in the library and the complete Filename.</param>
    /// <param name="assembly">The Assembly where the .boform File and the Code behind for the file is located. When null the assembly will be the calling assembly.</param>
    /// <returns></returns>
    public BoForm? ParseAndValidateBoFormFromResource(string resourceFileNameAndNamespace, Assembly? assembly = null)
    {
        if (assembly == null) assembly = Assembly.GetCallingAssembly();
        Stream? fileStream = assembly.GetManifestResourceStream(resourceFileNameAndNamespace);
        if (fileStream == null) throw new ArgumentException("Resource file doesn't exist");
        return TryToParseAndValidateBoForm(fileStream, assembly);
    }
    private BoForm TryToParseAndValidateBoForm(Stream fileStream, Assembly assembly)
    {
        BoForm form = TryToParseBoFromXml(fileStream);
        if(!new BoFormValidator().IsValidBoForm(form)) throw new Exception("BoForm is invalid");
        return form;
    }

    private BoForm TryToParseBoFromXml(Stream fileStream)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(BoForm));
        BoForm? form = (BoForm?)serializer.Deserialize(fileStream);
        if (form == null)
        {
            throw new FormatException("File could not be parsed. BoForm is null.");
        }
        return form;
    }
    
}