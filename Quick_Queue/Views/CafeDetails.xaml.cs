using Quick_Queue.Models;

namespace Quick_Queue.Views;

public partial class CafeDetails : ContentPage
{
    public CafeDetails()
    {
        InitializeComponent();

        BindingContext = new
        {
            CurrentCafe = SelectedCafeStore.CurrentCafe
        };
    }

    private async void OnCalculateWaitClicked(object sender, EventArgs e)
    {
        Cafe? cafe = SelectedCafeStore.CurrentCafe;

        if (cafe == null)
        {
            await DisplayAlert(
                "No Café Selected",
                "Please select a café first.",
                "OK");

            return;
        }

        if (!int.TryParse(PeopleWaitingEntry.Text, out int peopleWaiting))
        {
            await DisplayAlert(
                "Invalid Number",
                "Please enter a valid number of people waiting.",
                "OK");

            return;
        }

        if (peopleWaiting < 0)
        {
            await DisplayAlert(
                "Invalid Number",
                "The number of people cannot be negative.",
                "OK");

            return;
        }

        double estimatedWait = peopleWaiting * cafe.ServiceTime;

        string status;
        string recommendation;

        if (peopleWaiting <= 3)
        {
            status = "Quiet";
            recommendation = "Great time to grab something!";
        }
        else if (peopleWaiting <= 7)
        {
            status = "Moderate";
            recommendation = "The wait should be manageable.";
        }
        else if (peopleWaiting <= 10)
        {
            status = "Busy";
            recommendation = "You may want to come back later.";
        }
        else
        {
            status = "Very Busy";
            recommendation = "Consider trying another café.";
        }

        PeopleWaitingLabel.Text =
            $"People waiting: {peopleWaiting}";

        EstimatedWaitLabel.Text =
            $"Estimated wait: {estimatedWait:0.##} minutes";

        QueueStatusLabel.Text =
            $"Status: {status}";

        RecommendationLabel.Text =
            recommendation;

        QueueResultLayout.IsVisible = true;
    }
}