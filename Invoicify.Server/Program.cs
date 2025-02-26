using Invoicify.Server;
using Invoicify.Server.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddDbContext<InvoicifyDbContext>(options => options.UseSqlite("Data Source=Invoicify.db"));

var app = builder.Build();

app.MapGetEndpoints();
app.MapPostEndpoints();

await app.RunAsync();

