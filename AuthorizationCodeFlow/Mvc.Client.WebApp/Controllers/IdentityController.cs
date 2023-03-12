using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Mvc.Client.WebApp.Controllers;

public class IdentityController : Controller
{
    private readonly HttpClient httpClient;
    public IdentityController(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var content = await client.GetStringAsync("http://host.docker.internal:5037/WeatherForecast");
        ViewBag.Json = content.ToString();

        return View();
    }

    [Authorize]
    public IActionResult Logout()
    {
        return SignOut("Cookies", "oidc");
    }
}
