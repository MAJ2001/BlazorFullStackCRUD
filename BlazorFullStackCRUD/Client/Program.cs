global using BlazorFullStackCRUD.Client.Services.SuperVillainServices;
global using BlazorFullStackCRUD.Client.ComicServices;
global using BlazorFullStackCRUD.Shared;
using BlazorFullStackCRUD.Client;
using BlazorFullStackCRUD.Client.ComicServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Radzen.Blazor;
using BlazorFullStackCRUD.Client.Services.ComicsServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ISuperVillainService, SuperVillainService>();
builder.Services.AddScoped<IComicsServices , ComicsServices>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();



await builder.Build().RunAsync();
