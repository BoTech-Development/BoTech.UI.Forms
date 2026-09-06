using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Material.Icons;
using ReactiveUI;
using ReactiveUI.Primitives;

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

    public static readonly StyledProperty<byte> MaximumProperty =
        AvaloniaProperty.Register<StarInputControl, byte>(nameof(Maximum));

    public byte Maximum
    {
        get => GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value); 
    }

    public static readonly StyledProperty<byte> MinimumProperty =
    AvaloniaProperty.Register<StarInputControl, byte>(nameof(Minimum));

    public byte Minimum
    {
        get => GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
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

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == MaximumProperty || change.Property == MinimumProperty)
        {
            InitializeStars();
        }
    }

    private void InitializeStars()
    {
        List<StarItem> starsToDisplay = new List<StarItem>();
        for (int i = 0; i < Maximum; i++)
        {
            StarItem star;
            if (i < Minimum)
                star = StarItem.CreateConstantNotEditableStar(this, StarStatus.FullFilled);
            else
                star = new StarItem(this, StarStatus.Border);
            starsToDisplay.Add(star);
        }
        SetValue(StarsToDisplayProperty, new ObservableCollection<StarItem>(starsToDisplay));
    }
    private void OnStarClicked(StarItem onClickStar)
    {
        int countOfStarsToUpdate = StarsToDisplay.IndexOf(onClickStar);
        if(countOfStarsToUpdate == -1)
            throw new InvalidOperationException("Cannot find the star which the user selected.");
        UpdateStarSelection(countOfStarsToUpdate, NextStatus(onClickStar.Status));
    }

    private void UpdateStarSelection(int countOfFilledStars, StarStatus nextStarStatus)
    {
        UpdateValue(countOfFilledStars, nextStarStatus);
        UpdateValueVisually(countOfFilledStars, nextStarStatus);
    }

    private void UpdateValue(int countOfFilledStars, StarStatus nextStarStatus)
    {
        if (nextStarStatus == StarStatus.FullFilled)
        {
            CurrentValue = countOfFilledStars + 1;
        }
        else if (nextStarStatus == StarStatus.HalfFilled)
        {
            CurrentValue = countOfFilledStars + 0.5f;
        }
        else
        {
            CurrentValue = Minimum;
        }
        Console.WriteLine($"CurrentValue: {CurrentValue}; countOfFilledStars: {countOfFilledStars}");
    }
    private void UpdateValueVisually(int countOfFilledStars, StarStatus nextStarStatus)
    {
        StarStatus newStatus = StarStatus.Border;
        if(nextStarStatus == StarStatus.FullFilled || nextStarStatus == StarStatus.HalfFilled) // evaluate the status of the stars before the clicked star
            newStatus = StarStatus.FullFilled;
        // Fill all before if necessary
        for (int i = Minimum; i <= countOfFilledStars - 1; i++)
        {
            StarsToDisplay[i].Status = newStatus;
        }
        // fill clicked when necessary
        StarsToDisplay[countOfFilledStars].Status = nextStarStatus;
        // clear all after if there are any stars
        for (int i = countOfFilledStars + 1; i < Maximum; i++)
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

        public bool IsUnderlyingButtonEnabled { get => field; set => this.RaiseAndSetIfChanged(ref field, value); } = true;

        public ReactiveCommand<RxVoid, RxVoid> OnUpdateStarSelection { get; set; }
        public static StarItem CreateConstantNotEditableStar(StarInputControl control, StarStatus status)
        {
            return new StarItem(control, status)
            {
                IsUnderlyingButtonEnabled = false
            };
        }
        public StarItem(StarInputControl control, StarStatus status)
        {
            this.Status = status;
            OnUpdateStarSelection = ReactiveCommand.Create(() =>
            {
                control.OnStarClicked(this);
            });
        }
    }
}