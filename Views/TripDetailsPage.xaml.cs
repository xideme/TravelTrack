using TravelTrack.Models;
using TravelTrack.Views;

namespace TravelTrack.Views
{
    public partial class TripDetailsPage : ContentPage
    {
        private Trip _trip;

        // Constructor that takes the selected trip
        public TripDetailsPage(Trip trip)
        {
            InitializeComponent();
            _trip = trip;
            BindTripDetails();
        }

        private void BindTripDetails()
        {
            // Bind the trip details to UI controls
            NameLabel.Text = _trip.Name;
            DestinationLabel.Text = _trip.Destination;
            StartDateLabel.Text = _trip.StartDate.ToString("MMM dd, yyyy");
            EndDateLabel.Text = _trip.EndDate.ToString("MMM dd, yyyy");
            BudgetLabel.Text = _trip.Budget.ToString("C"); // Format as currency
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            // Navigate to EditTripPage to edit the trip details
            await Navigation.PushAsync(new EditTripPage(_trip));
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            // Confirm deletion
            var result = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this trip?", "Yes", "No");

            if (result)
            {
                // Delete the trip from the database
                await App.Database.DeleteTripAsync(_trip);
                await Navigation.PopAsync(); // Return to HomePage after deleting
            }

            var homePage = Navigation.NavigationStack.OfType<HomePage>().FirstOrDefault();
            if (homePage != null)
            {
                // Reload trips after deleting
                await homePage.LoadTrips();
            }
        }
    }
}
