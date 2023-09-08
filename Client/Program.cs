using Client;
using Client.Managers;
using Domain.Entities;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient<BaseManager<Contact>, ContactManager>();
builder.Services.AddScoped<PageHistoryNavigationManager>();
builder.Services.AddScoped<ProfileManager>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
