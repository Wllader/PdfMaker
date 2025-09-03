using System.Text;
using Data;
using Data.Bags;
using Data.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FileGenerator;
using Invoicify.Server.Views;

namespace Invoicify.Server.Endpoints;

/// <summary>
/// Extension methods for mapping HTTP endpoints for invoice operations.
/// </summary>
public static class EndpointMappingExtensions {
	/// <summary>
	/// Maps all GET endpoints for invoices, including HTML/PDF export and queries.
	/// </summary>
	/// <param name="app">WebApplication instance</param>
	public static void MapGetEndpoints(this WebApplication app) {
		// Returns all invoices with related entities (Seller, Customer, Bank, Items)
		app.MapGet("/invoice", async (InvoicifyDbContext db, [FromQuery] int top = 0) => {
			var invoices = db.Invoice
				.Include(i => i.SellerInfo)
				.Include(i => i.CustomerInfo)
				.Include(i => i.BankInfo)
				.Include(i => i.Items);
			
			var invoiceList = await invoices.ToListAsync();
			
			return Results.Ok(invoiceList);
		});
		
		// Returns a single invoice by ID with all details (including addresses and orders)
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

		// Returns invoice as rendered HTML for preview or export
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
		
		// Returns invoice as PDF file for download or print
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

		// Returns invoices as lightweight InvoiceBag objects for fast listing
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

		// Returns invoices filtered by number or variable symbol for search/autocomplete
		app.MapGet("/query/invoices", async (
			InvoicifyDbContext db, 
			[FromQuery] string number, 
			[FromQuery] string varSym,
			CancellationToken ct) => {
			if (string.IsNullOrWhiteSpace(number) && string.IsNullOrWhiteSpace(varSym)) {
				return Results.Ok(Array.Empty<InvoiceBag>());
			}

			var q = db.Invoice.AsNoTracking();
			
			if (!string.IsNullOrEmpty(number)) {
				q = q.Where(i => EF.Functions.Like(i.Number, number+"%"));
			}

			if (!string.IsNullOrEmpty(varSym)) {
				q = q.Where(i => EF.Functions.Like(i.VariableSymbol, varSym+"%"));
			}

			var invoices = await q
				// .OrderByDescending(i => i.CreatedAt)
				.Select(i => new InvoiceBag {
					Id = i.Id,
					Number = i.Number,
					IssueDate = i.IssueDate,
					DueDate = i.DueDate,

					CustomerName = i.CustomerInfo.FullName,
					SellerName = i.SellerInfo.FullName,

					CreatedAt = i.CreatedAt,
					UpdatedAt = i.UpdatedAt
				})
				.Take(20)
				.ToListAsync(ct);
			
			return Results.Ok(invoices);
		});
	}
	
	/// <summary>
	/// Maps POST endpoints for creating invoices.
	/// </summary>
	/// <param name="app">WebApplication instance</param>
	public static void MapPostEndpoints(this WebApplication app) {
		// Creates a new invoice and saves it to the database
		app.MapPost("/invoice", async (InvoicifyDbContext db, [FromBody] Invoice invoice) => {
			await db.Invoice.AddAsync(invoice);
			await db.SaveChangesAsync();
			return Results.Created($"/invoice/{invoice.Id}", invoice);
		});
	}
}