using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BookClient;
using BookClient.Services.Book;
using BookClient.Services.User;
using Simbad.State;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IBookClient, TestBookClient>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddStateManagement();

await builder.Build().RunAsync();