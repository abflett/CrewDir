using CrewDir.UIService;
using CrewDir.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

Uri apiUrl = new(builder.Configuration["ApiUrl"] ?? string.Empty);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = apiUrl });

builder.Services.AddScoped(crewDirApiClient =>
{
    var httpClient = crewDirApiClient.GetRequiredService<HttpClient>();
    return new CrewDirApiClient(apiUrl.ToString(), httpClient);
});

builder.Services.AddScoped<ApiClientService>();


await builder.Build().RunAsync();
