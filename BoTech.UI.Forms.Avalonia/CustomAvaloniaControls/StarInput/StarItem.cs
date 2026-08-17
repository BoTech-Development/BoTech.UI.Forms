using Avalonia.Interactivity;
using Material.Icons;
using ReactiveUI;
using ReactiveUI.Primitives;

namespace BoTech.UI.Forms.Avalonia.CustomAvaloniaControls.StarInput;

public class StarItem : ReactiveObject
{
    public StarStatus Status
    {
        get => field;
        set
        {
            if(value == StarStatus.FullFilled)
                IconToDisplay = MaterialIconKind.Star;
            else if(value == StarStatus.HalfFilled)
                IconToDisplay = MaterialIconKind.StarHalf;
            else if(value == StarStatus.Border)
                IconToDisplay = MaterialIconKind.StarBorder;
            field = value;
        }
    }
    public MaterialIconKind IconToDisplay { get => field; set => this.RaiseAndSetIfChanged(ref field, value); }

    public ReactiveCommand<RxVoid, RxVoid> OnUpdateStarSelection { get; set; }

    public StarItem(StarInputControl control, StarStatus status)
    {
        this.Status = status;
        OnUpdateStarSelection = ReactiveCommand.Create(() =>
        {
            control.OnStarClicked(this);
        });
    }
}