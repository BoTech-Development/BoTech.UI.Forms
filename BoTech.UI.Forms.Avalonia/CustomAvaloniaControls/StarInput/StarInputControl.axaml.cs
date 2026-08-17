using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Material.Icons;

namespace BoTech.UI.Forms.Avalonia.CustomAvaloniaControls.StarInput;

public class StarInputControl : TemplatedControl
{
    public static readonly StyledProperty<ObservableCollection<StarItem>> StarsToDisplayProperty =
        AvaloniaProperty.Register<StarInputControl, ObservableCollection<StarItem>>(nameof(StarsToDisplay));

    public ObservableCollection<StarItem> StarsToDisplay
    {
        get => GetValue(StarsToDisplayProperty);
        set => SetValue(StarsToDisplayProperty, value);
    }

    public StarInputControl()
    {
        SetValue(StarsToDisplayProperty, new ObservableCollection<StarItem>
        {
            new StarItem(this, StarStatus.FullFilled),
            new StarItem(this, StarStatus.FullFilled),
            new StarItem(this, StarStatus.FullFilled),
            new StarItem(this, StarStatus.HalfFilled),
            new StarItem(this, StarStatus.Border)
        });
    }

    public void OnStarClicked(StarItem onClickStar)
    {
        onClickStar.Status = NextStatus(onClickStar.Status);

        StarStatus newStatus = StarStatus.Border;
        if(onClickStar.Status == StarStatus.FullFilled || onClickStar.Status == StarStatus.HalfFilled) 
            newStatus = StarStatus.FullFilled;
        foreach (StarItem star in StarsToDisplay)
        {
            if (star == onClickStar)
            {
                newStatus = StarStatus.Border;
                continue;
            }
            star.Status = newStatus;
        }
    }

    private StarStatus NextStatus(StarStatus current)
    {
        switch (current)
        {
            case StarStatus.Border:
                return StarStatus.HalfFilled;
            case StarStatus.HalfFilled:
                return StarStatus.FullFilled;
            case StarStatus.FullFilled:
                return StarStatus.Border;
        }
        throw new ArgumentOutOfRangeException(nameof(current));
    }
}