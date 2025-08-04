using Invoicify.Server;
using Invoicify.Server.Endpoints;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

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
builder.Services.AddOpenApi();


var app = builder.Build();
app.UseCors("BlazorCorsPolicy");

if (app.Environment.IsDevelopment()) {
	app.MapOpenApi();
	app.MapScalarApiReference();
}

app.MapGetEndpoints();
app.MapPostEndpoints();

await app.RunAsync();

