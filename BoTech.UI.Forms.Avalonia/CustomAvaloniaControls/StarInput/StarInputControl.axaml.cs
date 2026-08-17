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

    public static readonly StyledProperty<int> CountOfStarsToDisplayProperty =
        AvaloniaProperty.Register<StarInputControl, int>(nameof(CountOfStarsToDisplay));

    public int CountOfStarsToDisplay
    {
        get =>
            GetValue(CountOfStarsToDisplayProperty);
        set
        {
            SetValue(CountOfStarsToDisplayProperty, value); 
            InitializeStars();
        }
    }
    
    public static readonly StyledProperty<float> CurrentValueProperty =
        AvaloniaProperty.Register<StarInputControl, float>(nameof(CurrentValue));

    public float CurrentValue
    {
        get => GetValue(CurrentValueProperty);
        set => SetValue(CurrentValueProperty, value); 
    }

    public StarInputControl()
    {
    }

    private void InitializeStars()
    {
        List<StarItem> starsToDisplay = new List<StarItem>();
        for (int i = 0; i < CountOfStarsToDisplay; i++)
        {
            StarItem star = new StarItem(this, StarStatus.Border);
            starsToDisplay.Add(star);
        }
        SetValue(StarsToDisplayProperty, new ObservableCollection<StarItem>(starsToDisplay));
    }
    public void OnStarClicked(StarItem onClickStar)
    {
        int countOfStarsToUpdate = StarsToDisplay.IndexOf(onClickStar);
        if(countOfStarsToUpdate == -1)
            throw new InvalidOperationException("Cannot find the star which the user selected.");
        UpdateStarSelection(countOfStarsToUpdate, NextStatus(onClickStar.Status));
    }

    private void UpdateStarSelection(int countOfFilledStars, StarStatus lastStarStatus)
    {
        UpdateValue(countOfFilledStars, lastStarStatus);
        UpdateValueVisually(countOfFilledStars, lastStarStatus);
    }

    private void UpdateValue(int countOfFilledStars, StarStatus lastStarStatus)
    {
        if (lastStarStatus == StarStatus.FullFilled)
        {
            CurrentValue = countOfFilledStars + 1;
        }
        else if (lastStarStatus == StarStatus.HalfFilled)
        {
            CurrentValue = countOfFilledStars + 0.5f;
        }
        else
        {
            CurrentValue = 0;
        }
        Console.WriteLine($"CurrentValue: {CurrentValue}; countOfFilledStars: {countOfFilledStars}");
    }
    private void UpdateValueVisually(int countOfFilledStars, StarStatus lastStarStatus)
    {
        StarStatus newStatus = StarStatus.Border;
        if(lastStarStatus == StarStatus.FullFilled || lastStarStatus == StarStatus.HalfFilled) 
            newStatus = StarStatus.FullFilled;
        // Fill all before if necessary
        for (int i = 0; i <= countOfFilledStars - 1; i++)
        {
            StarsToDisplay[i].Status = newStatus;
        }
        // fill clicked when necessary
        StarsToDisplay[countOfFilledStars].Status = lastStarStatus;
        // clear all after if there are any stars
        for (int i = countOfFilledStars + 1; i < CountOfStarsToDisplay; i++)
        {
            StarsToDisplay[i].Status = StarStatus.Border;
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