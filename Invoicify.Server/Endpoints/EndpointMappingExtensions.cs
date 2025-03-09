using Data;
using Data.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Invoicify.Server.Endpoints;

public static class EndpointMappingExtensions {
	public static void MapGetEndpoints(this WebApplication app) {
		app.MapGet("/invoice", async (InvoicifyDbContext db) => {
			var invoices = await db.Invoice.ToListAsync();
			return Results.Ok(invoices);
		});
		
		app.MapGet("/invoice/{id:guid}", async (InvoicifyDbContext db, Guid id) => {
			var invoice = await db.Invoice.FindAsync(id);
			return invoice == null ? Results.NotFound() : Results.Ok(invoice);
		});
	}
	
	public static void MapPostEndpoints(this WebApplication app) {
		app.MapPost("/invoice", async (InvoicifyDbContext db, [FromBody] Invoice invoice) => {
			await db.Invoice.AddAsync(invoice);
			await db.SaveChangesAsync();
			return Results.Created($"/invoice/{invoice.Id}", invoice);
		});
	}
}