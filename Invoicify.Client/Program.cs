using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Invoicify.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add root components and configure HTTP client for API communication
builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri("http://localhost:5225")});

await builder.Build().RunAsync();