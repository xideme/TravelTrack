using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTrack.Models;

namespace TravelTrack.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            try
            {

                _database = new SQLiteAsyncConnection(dbPath);
                InitializeDatabase();
            }
            catch (Exception ex)
            {
                // Log the exception for more details
                Console.WriteLine($"Error initializing database: {ex.Message}");
                throw;  // Rethrow to see the error details in the stack trace
            }
        }

        private async Task InitializeDatabase()
        {
            // Create the tables if they do not exist
            await _database.CreateTableAsync<Trip>();
            await _database.CreateTableAsync<Checklist>();  // Create table for checklist

        }

        // Insert a new trip
        public async Task<int> AddTripAsync(Trip trip)
        {
            return await _database.InsertAsync(trip);
        }

        // Get all trips
        public async Task<List<Trip>> GetAllTripsAsync()
        {
            return await _database.Table<Trip>().ToListAsync();
        }

        // Get a single trip by ID
        public async Task<Trip> GetTripByIdAsync(int id)
        {
            return await _database.FindAsync<Trip>(id);
        }

        // Update a trip
        public async Task<int> UpdateTripAsync(Trip trip)
        {
            return await _database.UpdateAsync(trip);
        }

        // Delete a trip
        // In DatabaseService.cs
        public async Task<int> DeleteTripAsync(Trip trip)
        {
            // Use the trip ID to delete the specific trip
            return await _database.DeleteAsync(trip);
        }

        // Add a checklist item
        public Task<int> AddChecklistItemAsync(Checklist checklist)
        {
            return _database.InsertAsync(checklist);
        }

        // Get all checklist items for a specific trip
        public Task<List<Checklist>> GetChecklistItemsAsync(int tripId)
        {
            return _database.Table<Checklist>()
                             .Where(c => c.TripId == tripId)
                             .ToListAsync();
        }

        public async Task UpdateChecklistAsync(Checklist checklist)
        {
            await _database.UpdateAsync(checklist);
        }

        public async Task DeleteChecklistItemAsync(Checklist checklist)
        {
            // Delete the checklist item from the database
            await _database.DeleteAsync(checklist);
        }

    }
}

