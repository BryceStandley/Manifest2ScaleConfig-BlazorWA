using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Manifest2ScaleConfig_BlazorWA;
using Manifest2ScaleConfig_BlazorWA.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register services
builder.Services.AddScoped<XmlService>();
builder.Services.AddScoped<CsvService>();
builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
