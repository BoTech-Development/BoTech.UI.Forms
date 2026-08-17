using System.Numerics;
using BoTech.UI.Forms.Controls.Layout;

namespace BoTech.UI.Forms.Controls.Input.Numeric;

public interface ISliderInput<T>  : INumberInput<T> where T : INumber<T>
{
    public TickPlacement TickPlacement { get; set; }
    //public T LargeChangeInr
    public Orientation Orientation { get; set; }
    public bool TickMarks { get; set; }
}