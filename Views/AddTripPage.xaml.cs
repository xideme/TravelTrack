using TravelTrack.Models;
using TravelTrack.Services;

namespace TravelTrack.Views
{
    public partial class AddTripPage : ContentPage
    {
        public AddTripPage()
        {
            InitializeComponent();
        }

        // This method handles saving a new trip and navigating back
        public async void OnSaveTripClicked(object sender, EventArgs e)
        {
            double budget = double.TryParse(BudgetEntry.Text, out double parsedBudget) ? parsedBudget : 0;


            var newTrip = new Trip
            {
                Name = NameEntry.Text,
                Destination = DestinationEntry.Text,
                StartDate = StartDatePicker.Date,
                EndDate = EndDatePicker.Date,
                Budget = budget // Set Budget as double

            };

            // Add the new trip to the database
            await App.Database.AddTripAsync(newTrip);

            // Navigate back to HomePage
            await Navigation.PopAsync();

            
        }
    }
}
