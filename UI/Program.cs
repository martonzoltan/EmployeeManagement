using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Shared.Mappers;
using UI;
using UI.Services;
using UI.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient {BaseAddress = new Uri("https://localhost:7123/")});
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddMudServices();
builder.Services.AddAutoMapper(typeof(MappingProfile));

await builder.Build().RunAsync();