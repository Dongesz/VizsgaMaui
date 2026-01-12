using System.Text.Json;

namespace VizsgaMaui;

public partial class UserDetailPage : ContentPage
{
    private readonly int _id;

    public UserDetailPage(int id)
    {
        InitializeComponent();
        _id = id;
        LoadUser();
    }

    private async void LoadUser()
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri("https://dongesz.com/")
        };

        var json = await client.GetStringAsync($"api/Users/{_id}");

        var response = JsonSerializer.Deserialize<ApiResponseSingle>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var u = response!.result;

        ProfileImage.Source = u.profilePictureUrl;
        NameLabel.Text = u.name;
        EmailLabel.Text = u.email;
        BioLabel.Text = u.bio;
        UserTypeLabel.Text = u.userType;
    }
}

public class ApiResponseSingle
{
    public string message { get; set; }
    public bool success { get; set; }
    public User result { get; set; }
}
