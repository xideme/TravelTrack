using TravelTrack.Models;

namespace TravelTrack.Views
{
    public partial class EditTripPage : ContentPage
    {
        public Trip Trip { get; set; }

        public EditTripPage(Trip trip)
        {
            InitializeComponent();
            Trip = trip;
            BindingContext = this; // Set the BindingContext to this page
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Update the trip details
            await App.Database.UpdateTripAsync(Trip);
            await Navigation.PopToRootAsync(); // Return to the previous page
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            // Cancel the editing and return to the previous page
            await Navigation.PopAsync();
        }
    }
}
