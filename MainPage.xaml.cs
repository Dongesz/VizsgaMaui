using System.Text.Json;

namespace VizsgaMaui;

public partial class MainPage : ContentPage
{
    public List<User> Users { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();
        LoadUsers();
    }

    private async void LoadUsers()
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri("https://dongesz.com/")
        };

        var json = await client.GetStringAsync("api/Users");

        var response = JsonSerializer.Deserialize<ApiResponse>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Users = response!.result;
        dataGrid.ItemsSource = Users;
    }

    private async void OnUserTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is int id)
            await Navigation.PushAsync(new UserDetailPage(id));
    }

    private void OnLikeClicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var user = (User)btn.BindingContext;
        user.IsLiked = !user.IsLiked;
    }
}

public class ApiResponse
{
    public string message { get; set; }
    public bool success { get; set; }
    public List<User> result { get; set; }
}

public class User
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string bio { get; set; }
    public string userType { get; set; }
    public string profilePictureUrl { get; set; }

    public bool IsLiked { get; set; }
}

