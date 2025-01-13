using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Invoicify;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddDbContext<InvoicifyDbContext>(options => 
	options.UseSqlite("Data Source=Invoicify.db")
);


await builder.Build().RunAsync();

var sql = new SqliteConnection("Data Source=Invoicify.db");