using Data.DbModel;
using Data.DbModel.BaseTypes;
using Microsoft.EntityFrameworkCore;

namespace Invoicify.Server;

/// <summary>
/// Entity Framework database context for the Invoicify application.
/// </summary>
public class InvoicifyDbContext : DbContext{
	/// <summary>
	/// Initializes the database context with options.
	/// </summary>
	/// <param name="options">DbContext options</param>
	public InvoicifyDbContext(DbContextOptions<InvoicifyDbContext> options) : base(options) { }
	
	/// <summary>
	/// Table of invoices.
	/// </summary>
	public DbSet<Invoice> Invoice { get; set; }
	/// <summary>
	/// Table of parties (customers/sellers).
	/// </summary>
	public DbSet<PartyInfo> PartyInfo { get; set; }
	/// <summary>
	/// Table of addresses.
	/// </summary>
	public DbSet<Address> Address { get; set; }
	/// <summary>
	/// Table of bank information.
	/// </summary>
	public DbSet<BankInfo> BankInfo { get; set; }
	/// <summary>
	/// Table of orders.
	/// </summary>
	public DbSet<OrderInfo> OrderInfo { get; set; }
	/// <summary>
	/// Table of invoice items.
	/// </summary>
	public DbSet<InvoiceItem> InvoiceItem { get; set; }

	/// <summary>
	/// Saves changes asynchronously and updates timestamps and QR codes.
	/// </summary>
	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
		ModifyUpdate();
		return await base.SaveChangesAsync(cancellationToken);
	}

	/// <summary>
	/// Saves changes and updates timestamps and QR codes.
	/// </summary>
	public override int SaveChanges() {
		ModifyUpdate();
		return base.SaveChanges();
	}

	/// <summary>
	/// Updates timestamps and QR code SPR string for modified entities.
	/// </summary>
	private void ModifyUpdate() {
		foreach (var entry in ChangeTracker.Entries<TimeStampedEntity>()) {
			if(entry.State == EntityState.Modified)
				entry.Entity.UpdatedAt = DateTimeOffset.Now;
		}
		
		UpdateQrCodeSpr();
	}

	/// <summary>
	/// Updates the QrSpr property for invoices when modified or added.
	/// </summary>
	private void UpdateQrCodeSpr() {
		foreach (var entry in ChangeTracker.Entries<Invoice>()) {
			if (entry.State is not (EntityState.Modified or EntityState.Added)) continue;
			var invoice = entry.Entity;
			invoice.QrSpr = invoice.QrPayment?.GetSpr();
		}
	}
	
}