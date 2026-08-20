using Microsoft.Maui.Controls.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Skip)]

namespace Quick_Queue.Views;

public partial class HomePage : ContentPage
{
    private Picker cafePicker = null!;
    private Entry peopleInput = null!;
    private Stepper peopleStepper = null!;
    private Border resultsCard = null!;
    private Label waitTimeLabel = null!;
    private Label statusLabel = null!;
    private Frame statusBadge = null!;
    private Label recLabel = null!;
    private Frame recFrame = null!;

    public HomePage()
    {
        this.BackgroundColor = Color.FromArgb("#F5F7FA");
        BuildUI();
    }

    private void BuildUI()
    {
        // Header Banner
        var headerLabel = new Label
        {
            Text = "⚡ QuickQueue",
            FontSize = 26,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center
        };

        var subHeaderLabel = new Label
        {
            Text = "Live Campus Queue & Wait Time Estimator",
            FontSize = 13,
            TextColor = Color.FromArgb("#E3F2FD"),
            HorizontalOptions = LayoutOptions.Center
        };

        var headerFrame = new Frame
        {
            BackgroundColor = Color.FromArgb("#1E88E5"),
            CornerRadius = 12,
            HasShadow = true,
            Padding = 20,
            Content = new VerticalStackLayout { Spacing = 5, Children = { headerLabel, subHeaderLabel } }
        };

        // Inputs Card
        cafePicker = new Picker
        {
            Title = "Choose a Café",
            TitleColor = Color.FromArgb("#888888"),
            ItemsSource = new List<string> { "Main Campus Café", "Library Café", "Student Hub Café" }
        };
        cafePicker.SelectedIndexChanged += OnInputChanged;

        peopleInput = new Entry
        {
            Keyboard = Keyboard.Numeric,
            Text = "0"
        };
        peopleInput.TextChanged += OnInputChanged;

        peopleStepper = new Stepper
        {
            Minimum = 0,
            Maximum = 100,
            Increment = 1,
            Value = 0
        };
        peopleStepper.ValueChanged += OnStepperValueChanged;

        var stepperGrid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            },
            ColumnSpacing = 10
        };
        stepperGrid.Add(peopleInput, 0, 0);
        stepperGrid.Add(peopleStepper, 1, 0);

        var calcButton = new Button
        {
            Text = "Calculate",
            BackgroundColor = Color.FromArgb("#1E88E5"),
            TextColor = Colors.White,
            FontAttributes = FontAttributes.Bold,
            CornerRadius = 8
        };
        calcButton.Clicked += OnCalculateClicked;

        var resetButton = new Button
        {
            Text = "Reset",
            BackgroundColor = Color.FromArgb("#E0E0E0"),
            TextColor = Color.FromArgb("#333333"),
            FontAttributes = FontAttributes.Bold,
            CornerRadius = 8
        };
        resetButton.Clicked += OnResetClicked;

        var buttonGrid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            },
            ColumnSpacing = 10,
            Margin = new Thickness(0, 10, 0, 0)
        };
        buttonGrid.Add(calcButton, 0, 0);
        buttonGrid.Add(resetButton, 1, 0);

        var inputCard = new Border
        {
            Stroke = Color.FromArgb("#E0E0E0"),
            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 12 },
            Padding = 15,
            BackgroundColor = Colors.White,
            Content = new VerticalStackLayout
            {
                Spacing = 15,
                Children =
                {
                    new Label { Text = "Select Location", FontAttributes = FontAttributes.Bold, FontSize = 14, TextColor = Color.FromArgb("#333333") },
                    cafePicker,
                    new Label { Text = "People Currently Waiting", FontAttributes = FontAttributes.Bold, FontSize = 14, TextColor = Color.FromArgb("#333333") },
                    stepperGrid,
                    buttonGrid
                }
            }
        };

        // Output Results Card
        waitTimeLabel = new Label { Text = "0", FontSize = 36, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#1E88E5") };
        statusLabel = new Label { Text = "Quiet", FontAttributes = FontAttributes.Bold, FontSize = 12 };
        statusBadge = new Frame { Padding = new Thickness(10, 4), CornerRadius = 12, HasShadow = false, BorderColor = Colors.Transparent, Content = statusLabel };
        recLabel = new Label { Text = "--", FontSize = 13, TextColor = Color.FromArgb("#333333"), HorizontalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.Center };
        recFrame = new Frame { BackgroundColor = Color.FromArgb("#F5F5F5"), Padding = 12, CornerRadius = 8, HasShadow = false, BorderColor = Colors.Transparent, Content = recLabel };

        var waitTimeStack = new HorizontalStackLayout
        {
            Spacing = 5,
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                waitTimeLabel,
                new Label { Text = "mins", FontSize = 18, TextColor = Color.FromArgb("#666666"), VerticalOptions = LayoutOptions.End, Margin = new Thickness(0,0,0,5) }
            }
        };

        var statusStack = new HorizontalStackLayout
        {
            Spacing = 10,
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                new Label { Text = "Status:", FontSize = 14, TextColor = Color.FromArgb("#555555"), VerticalOptions = LayoutOptions.Center },
                statusBadge
            }
        };

        resultsCard = new Border
        {
            Stroke = Color.FromArgb("#CCCCCC"),
            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 12 },
            Padding = 20,
            BackgroundColor = Colors.White,
            IsVisible = false,
            Content = new VerticalStackLayout
            {
                Spacing = 12,
                Children =
                {
                    new Label { Text = "ESTIMATED WAIT", FontSize = 11, CharacterSpacing = 1, TextColor = Color.FromArgb("#666666"), FontAttributes = FontAttributes.Bold },
                    waitTimeStack,
                    new BoxView { HeightRequest = 1, Color = Color.FromArgb("#EEEEEE") },
                    statusStack,
                    recFrame
                }
            }
        };

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 20,
                MaximumWidthRequest = 500,
                Children = { headerFrame, inputCard, resultsCard }
            }
        };
    }

    private void OnStepperValueChanged(object? sender, ValueChangedEventArgs e)
    {
        if (peopleInput != null)
            peopleInput.Text = e.NewValue.ToString();
    }

    private void OnInputChanged(object? sender, EventArgs e)
    {
        if (peopleInput != null && peopleStepper != null && int.TryParse(peopleInput.Text, out int val) && val >= 0)
        {
            peopleStepper.Value = val;
        }
    }

    private void OnCalculateClicked(object? sender, EventArgs e)
    {
        if (cafePicker.SelectedIndex == -1)
        {
            DisplayAlert("Selection Required", "Please select a café location first.", "OK");
            return;
        }

        if (!int.TryParse(peopleInput.Text, out int peopleCount) || peopleCount < 0)
        {
            DisplayAlert("Invalid Input", "Please enter a valid non-negative number of people.", "OK");
            return;
        }

        double serviceTime = cafePicker.SelectedItem?.ToString() switch
        {
            "Main Campus Café" => 2.0,
            "Library Café" => 3.0,
            "Student Hub Café" => 1.5,
            _ => 2.0
        };

        double totalWait = peopleCount * serviceTime;
        string status = GetQueueStatus(peopleCount);
        string recommendation = GetRecommendation(status);

        waitTimeLabel.Text = totalWait.ToString("0.#");
        statusLabel.Text = status;
        recLabel.Text = recommendation;

        ApplyStatusStyling(status);
        resultsCard.IsVisible = true;
    }

    private string GetQueueStatus(int count) => count switch
    {
        <= 3 => "Quiet",
        <= 7 => "Moderate",
        <= 10 => "Busy",
        _ => "Very Busy"
    };

    private string GetRecommendation(string status) => status switch
    {
        "Quiet" => "Great time to grab something!",
        "Moderate" => "The wait should be manageable.",
        "Busy" => "You may want to come back later.",
        "Very Busy" => "Consider trying another café.",
        _ => ""
    };

    private void ApplyStatusStyling(string status)
    {
        switch (status)
        {
            case "Quiet":
                statusBadge.BackgroundColor = Color.FromArgb("#E8F5E9");
                statusLabel.TextColor = Color.FromArgb("#2E7D32");
                recFrame.BackgroundColor = Color.FromArgb("#E8F5E9");
                break;
            case "Moderate":
                statusBadge.BackgroundColor = Color.FromArgb("#FFFDE7");
                statusLabel.TextColor = Color.FromArgb("#F57F17");
                recFrame.BackgroundColor = Color.FromArgb("#FFFDE7");
                break;
            case "Busy":
                statusBadge.BackgroundColor = Color.FromArgb("#FFF3E0");
                statusLabel.TextColor = Color.FromArgb("#E65100");
                recFrame.BackgroundColor = Color.FromArgb("#FFF3E0");
                break;
            case "Very Busy":
                statusBadge.BackgroundColor = Color.FromArgb("#FFEBEE");
                statusLabel.TextColor = Color.FromArgb("#C62828");
                recFrame.BackgroundColor = Color.FromArgb("#FFEBEE");
                break;
        }
    }

    private void OnResetClicked(object? sender, EventArgs e)
    {
        cafePicker.SelectedIndex = -1;
        peopleInput.Text = "0";
        peopleStepper.Value = 0;
        resultsCard.IsVisible = false;
    }
}

