using System.Numerics;
using BoTech.UI.Forms.Rendering;

namespace BoTech.UI.Forms.Controls.Input.Numeric;

public interface INumberInput<T> : IInput<T> where T : INumber<T>
{
    public T Maximum { get; set; }
    public T Minimum { get; set; }
    public T Increment { get; set; }
}