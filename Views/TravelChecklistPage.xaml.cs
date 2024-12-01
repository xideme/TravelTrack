using TravelTrack.Models;
using System;
using System.Linq;
using TravelTrack.Services;

namespace TravelTrack.Views
{
    public partial class TravelChecklistPage : ContentPage
    {
        private int _selectedTripId;

        public Command<Checklist> RemoveChecklistItem { get; }

        public TravelChecklistPage()
        {
            InitializeComponent();
            LoadTrips();
            BindingContext = this;
            RemoveChecklistItem = new Command<Checklist>(OnRemoveChecklistItem);

        }

        private async void LoadTrips()
        {
            // Get all trips from the database
            var trips = await App.Database.GetAllTripsAsync();
            TripListView.ItemsSource = trips;
        }

        private async void OnTripSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0)
            {
                // Get the selected trip
                var selectedTrip = (Trip)e.CurrentSelection.FirstOrDefault();
                if (selectedTrip != null)
                {
                    _selectedTripId = selectedTrip.TripId;

                    // Load checklist items for the selected trip
                    var checklistItems = await App.Database.GetChecklistItemsAsync(_selectedTripId);
                    ChecklistView.ItemsSource = checklistItems;

                    // Enable the "Add Checklist Item" button
                    AddChecklistButton.IsEnabled = true;
                }
            }
        }

        private async void OnAddChecklistItemClicked(object sender, EventArgs e)
        {
            // Prompt the user to enter a checklist item name
            var itemName = await DisplayPromptAsync("New Checklist Item", "Enter checklist item name:");

            if (!string.IsNullOrWhiteSpace(itemName))
            {
                var newItem = new Checklist
                {
                    TripId = _selectedTripId,
                    Name = itemName,
                    IsChecked = false
                };

                // Add the new checklist item to the database
                await App.Database.AddChecklistItemAsync(newItem);

                // Reload the checklist for the selected trip
                var checklistItems = await App.Database.GetChecklistItemsAsync(_selectedTripId);
                ChecklistView.ItemsSource = checklistItems;

            }
        }

        private async void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
           if (sender is CheckBox checkBox && checkBox.BindingContext is Checklist checklist)
               {
                  checklist.IsChecked = e.Value; // Update the model
                  await App.Database.UpdateChecklistAsync(checklist); // Save to the database
               }
        }

        private async void OnRemoveChecklistItem(Checklist checklist)
        {
            if (checklist != null)
            {
                // Delete the checklist item from the database
                await App.Database.DeleteChecklistItemAsync(checklist);

                // Reload the checklist for the selected trip
                var checklistItems = await App.Database.GetChecklistItemsAsync(_selectedTripId);
                ChecklistView.ItemsSource = checklistItems;
            }
        }

    }
}

