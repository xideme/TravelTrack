using TravelTrack.Services;
using TravelTrack.Views;

namespace TravelTrack
{
    public partial class App : Application
    {

        public static DatabaseService Database { get; private set; }

        public App()
        {
            InitializeComponent();

            // Initialize the DatabaseService

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "travelplanner.db");
            Database = new DatabaseService(dbPath);


            // Set the MainPage as a NavigationPage
            MainPage = new NavigationPage(new HomePage());

        }
    }
}