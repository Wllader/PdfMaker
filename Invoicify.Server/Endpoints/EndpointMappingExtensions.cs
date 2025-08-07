using System.Text;
using Data;
using Data.Bags;
using Data.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FileGenerator;
using Invoicify.Server.Views;

namespace Invoicify.Server.Endpoints;

public static class EndpointMappingExtensions {
	public static void MapGetEndpoints(this WebApplication app) {
		app.MapGet("/invoice", async (InvoicifyDbContext db, [FromQuery] int top = 0) => {
			var invoices = db.Invoice
				.Include(i => i.SellerInfo)
				.Include(i => i.CustomerInfo)
				.Include(i => i.BankInfo)
				.Include(i => i.Items);
			
			var invoiceList = await invoices.ToListAsync();
			
			return Results.Ok(invoiceList);
		});
		
		app.MapGet("/invoice/{id:guid}", async (InvoicifyDbContext db, Guid id) => {
			var invoice = await db.Invoice
				.Include(i => i.SellerInfo).ThenInclude(si => si.Address)
				.Include(i => i.CustomerInfo).ThenInclude(ci => ci.Address)
				.Include(i => i.BankInfo)
				.Include(i => i.Items)
				.Include(i => i.OrdersInfo)
				.Where(i => i.Id == id)
				.FirstOrDefaultAsync();
			return invoice == null ? Results.NotFound() : Results.Ok(invoice);
		});

		app.MapGet("/invoice/{id:guid}/html", async (InvoicifyDbContext db, Guid id) => {
			var invoice = await db.Invoice
				.Include(i => i.SellerInfo).ThenInclude(si => si.Address)
				.Include(i => i.CustomerInfo).ThenInclude(ci => ci.Address)
				.Include(i => i.BankInfo)
				.Include(i => i.Items)
				.Include(i => i.OrdersInfo)
				.Where(i => i.Id == id)
				.FirstOrDefaultAsync();


			if (invoice is null)
				return Results.NotFound();
			
			if (invoice.QrSpr is not null)
				invoice.QrPayment = QrPayment.FromSprString(invoice.QrSpr);
			
			var generator = new DocumentGenerator<InvoiceView>(invoice);
			string html = await generator.GetHtmlAsync();
			return Results.Content(
				content: html,
				contentType: "text/html",
				contentEncoding: Encoding.UTF8
			);
		});
		
		app.MapGet("/invoice/{id:guid}/pdf", async (InvoicifyDbContext db, Guid id) => {
			var invoice = await db.Invoice
				.Include(i => i.SellerInfo).ThenInclude(si => si.Address)
				.Include(i => i.CustomerInfo).ThenInclude(ci => ci.Address)
				.Include(i => i.BankInfo)
				.Include(i => i.Items)
				.Include(i => i.OrdersInfo)
				.Where(i => i.Id == id)
				.FirstOrDefaultAsync();


			if (invoice is null)
				return Results.NotFound();

			if (invoice.QrSpr is not null)
				invoice.QrPayment = QrPayment.FromSprString(invoice.QrSpr);
			
			var generator = new DocumentGenerator<InvoiceView>(invoice);
			var pdf = await generator.GetPdfAsync();

			return Results.File(
				fileContents: pdf,
				contentType: "application/pdf",
				fileDownloadName: $"{invoice.Number}.pdf"
			);
		});

		app.MapGet("/asbag/invoice", async (InvoicifyDbContext db, [FromQuery] int top) => {
			var data = db.Invoice
				.Include(i => i.SellerInfo)
				.Include(i => i.CustomerInfo)
				.Include(i => i.BankInfo)
				.Select(i => new InvoiceBag {
					Id = i.Id,
					Number = i.Number,
					IssueDate = i.IssueDate,
					DueDate = i.DueDate,
					
					CustomerName = i.CustomerInfo.FullName,
					SellerName = i.SellerInfo.FullName,
					
					CreatedAt = i.CreatedAt,
					UpdatedAt = i.UpdatedAt
				});
			
			return Results.Ok(await data.ToListAsync());
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