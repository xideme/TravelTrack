using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelTrack.Models
{
    public class Checklist
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TripId { get; set; }  // Foreign key to the Trip
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}
