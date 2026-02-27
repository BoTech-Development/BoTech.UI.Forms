using BoTech.UI.Forms.Controls;

namespace BoTech.UI.Forms.Models.Import;

public class ViewModelImport : ImportBase
{
    public string Namespace { get; init; } = string.Empty;
    public string ClassName { get; init; } = string.Empty;
    public string As { get; init; } = string.Empty;

}