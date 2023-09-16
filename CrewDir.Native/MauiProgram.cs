using CrewDir.UIService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CrewDir.Native
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            builder.Configuration.AddConfiguration(config);

            Uri apiUrl = new(builder.Configuration["ApiUrl"] ?? string.Empty);

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = apiUrl });

            builder.Services.AddScoped(crewDirApiClient =>
            {
                var httpClient = crewDirApiClient.GetRequiredService<HttpClient>();
                return new CrewDirApiClient(apiUrl.ToString(), httpClient);
            });

            builder.Services.AddScoped<ApiClientService>();

            return builder.Build();
        }
    }
}
