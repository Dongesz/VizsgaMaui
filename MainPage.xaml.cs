using System.Text.Json;

namespace VizsgaMaui
{
    public partial class MainPage : ContentPage
    {
        public List<User> Users { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();
            LoadUsers();
        }
        public class ApiResponse
        {
            public string message { get; set; }
            public bool success { get; set; }
            public List<User> result { get; set; } = new();
        }

        public class User
        {
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string bio { get; set; }
            public string userType { get; set; }
            public string profilePictureUrl { get; set; }
            public string createdAt { get; set; }
            public string updatedAt { get; set; }
        }
        private async void LoadUsers()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://dongesz.com/");

            try
            {
                var response = await httpClient.GetStringAsync("api/Users");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(response, options);

                Users = apiResponse.result;
                dataGrid.ItemsSource = Users;

                await DisplayAlert("Sikeres", $"{Users.Count} user betöltve", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hiba", ex.Message, "OK");
            }
        }
    }
}