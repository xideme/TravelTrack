using TravelTrack.Models;
using TravelTrack.Views;

namespace TravelTrack.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadTrips();
        }

        public async Task LoadTrips()
        {

            // Load trips from the database and bind to the CollectionView
            var trips = await App.Database.GetAllTripsAsync();

            // Set the trips as the data source for the CollectionView
            TripListView.ItemsSource = trips;


            // Calculate the nearest trip
            var nearestTrip = trips
                .Where(t => t.StartDate > DateTime.Now)  // Filter trips that start in the future
                .OrderBy(t => t.StartDate)               // Order by the nearest start date
                .FirstOrDefault();

            if (nearestTrip != null)
            {
                // Calculate the countdown to the nearest trip
                var daysRemaining = (nearestTrip.StartDate - DateTime.Now).Days;
                CountdownLabel.Text = $"Next trip in {daysRemaining} days: {nearestTrip.Name}";
            }
            else
            {
                CountdownLabel.Text = "No upcoming trips.";
            }

        }

        public async void OnAddTripClicked(object sender, EventArgs e)
        {
            // Navigate to AddTripPage to add a new trip
            await Navigation.PushAsync(new AddTripPage());
        }

        private async void OnTravelChecklistClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TravelChecklistPage());
        }

        public async void OnTripSelected(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("SelectionChanged triggered.");

            if (e.CurrentSelection.Count > 0)
            {
                // Get the selected trip
                var selectedTrip = (Trip)e.CurrentSelection.FirstOrDefault();

                if (selectedTrip != null)
                {
                    Console.WriteLine("Trip selected: " + e.CurrentSelection.FirstOrDefault()?.ToString());
                    // Navigate to TripDetailsPage, passing the selected trip
                    await Navigation.PushAsync(new TripDetailsPage(selectedTrip));

                    // Clear selection (optional)
                    ((CollectionView)sender).SelectedItem = null;
                }
            }
        }
    }
}
