using BlazorWasmApp1;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorWasmApp1;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        //builder.Services.AddOidcAuthentication(options =>
        //{
        //    // Configure your authentication provider options here.
        //    // For more information, see https://aka.ms/blazor-standalone-auth
        //    builder.Configuration.Bind("Local", options.ProviderOptions);
        //});

        builder.Services.AddOidcAuthentication(options =>
        {
            // Configure your authentication provider options here.
            // For more information, see https://aka.ms/blazor-standalone-auth
            options.ProviderOptions.Authority = builder.Configuration["IdentityUrl"];
            options.ProviderOptions.ClientId = "blazorwasmapp1";
            options.ProviderOptions.ResponseType = "code";
        });

        await builder.Build().RunAsync();
    }
}
