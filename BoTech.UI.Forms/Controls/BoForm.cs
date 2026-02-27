using BoTech.UI.Forms.Models.Import;

namespace BoTech.UI.Forms.Controls;

public class BoForm
{
    public Form FormContent { get; init; } = new();
    public List<ImportBase> Imports { get; init; } = new List<ImportBase>();
}