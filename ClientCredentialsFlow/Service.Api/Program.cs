
using System.IdentityModel.Tokens.Jwt;

namespace Service.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        ConfigureServices(builder);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    static void ConfigureAuthService(WebApplicationBuilder builder)
    {
        // prevent from mapping "sub" claim to nameidentifier.
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
        {
            options.Authority = builder.Configuration["IdentityUrl"];

            // For the development environment this can be false.
            options.RequireHttpsMetadata = false;
            options.Audience = "serviceapi";
            options.TokenValidationParameters.ValidateAudience = false;
        });
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                //All requests to the service need to be authenticated
                policy.RequireAuthenticatedUser();

                //All requests to the service need to have access to the serviceapi scope.
                policy.RequireClaim("scope", "serviceapi");
            });
        });
    }
}
