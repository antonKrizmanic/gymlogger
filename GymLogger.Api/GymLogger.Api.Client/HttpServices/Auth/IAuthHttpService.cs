using System.Text;

namespace GymLogger.Api.Client.HttpServices.Auth;

public interface IAuthHttpService
{
    Task SignOutAsync(string returnUrl);
}

public class AuthHttpService: IAuthHttpService
{
    private readonly HttpClient _httpClient;

    public AuthHttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("BlazorServerHttpClient");
    }

    public async Task SignOutAsync(string returnUrl)
    {
        var response = await _httpClient.PostAsync("Account/Logout", null);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new ApplicationException(content);
    }
}