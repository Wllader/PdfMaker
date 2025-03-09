using Invoicify.Server;
using Invoicify.Server.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddDbContext<InvoicifyDbContext>(options => options.UseSqlite("Data Source=Invoicify.db"));
builder.Services.AddCors(options =>
{
	options.AddPolicy("BlazorCorsPolicy", policy =>
	{
		policy.WithOrigins("http://localhost:5125") // Change to your Blazor app URL
			.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowCredentials();
	});
});


var app = builder.Build();
app.UseCors("BlazorCorsPolicy");

app.MapGetEndpoints();
app.MapPostEndpoints();

await app.RunAsync();

