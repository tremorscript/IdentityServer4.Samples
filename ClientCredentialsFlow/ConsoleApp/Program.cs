using IdentityModel.Client;
using IdentityModel.OidcClient;
using Newtonsoft.Json.Linq;

namespace ConsoleApp;

internal class Program
{
    static string _authority = "https://localhost:5001/";
    static string _api = "https://localhost:7299/WeatherForecast";

    static OidcClient _oidcClient;
    static HttpClient _apiClient = new HttpClient { BaseAddress = new Uri(_api) };

    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        await Login();
    }

    private static async Task Login()
    {
        var browser = new SystemBrowser();
        string redirectUri = string.Format($"http://127.0.0.1:{browser.Port}");

        var options = new OidcClientOptions
        {
            Authority = _authority,
            ClientId = "consoleapp",
            RedirectUri = redirectUri,
            Scope = "openid profile serviceapi",
            FilterClaims = false,
            Browser = browser
        };

        _oidcClient = new OidcClient(options);
        var result = await _oidcClient.LoginAsync(new LoginRequest());
        ShowResult(result);
    }

    private static void ShowResult(LoginResult result)
    {
        if (result.IsError)
        {
            Console.WriteLine("\n\nError:\n{0}", result.Error);
            return;
        }

        Console.WriteLine("\n\nClaims:");
        foreach (var claim in result.User.Claims)
        {
            Console.WriteLine("{0}: {1}", claim.Type, claim.Value);
        }

        Console.WriteLine($"\nidentity token: {result.IdentityToken}");
        Console.WriteLine($"access token:   {result.AccessToken}");
        Console.WriteLine($"refresh token:  {result?.RefreshToken ?? "none"}");
    }

    private static async Task CallApi(string currentAccessToken)
    {
        _apiClient.SetBearerToken(currentAccessToken);
        var response = await _apiClient.GetAsync("");

        if (response.IsSuccessStatusCode)
        {
            var json = JArray.Parse(await response.Content.ReadAsStringAsync());
            Console.WriteLine("\n\n");
            Console.WriteLine(json);
        }
        else
        {
            Console.WriteLine($"Error: {response.ReasonPhrase}");
        }
    }

}
