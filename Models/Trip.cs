using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelTrack.Models
{
    public class Trip
    {
        [PrimaryKey, AutoIncrement]
        public int TripId { get; set; }
        public string Name { get; set; }
        public string Destination { get; set; }
        public double Budget { get; set; } // New property for budget
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
