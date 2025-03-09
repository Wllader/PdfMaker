using Data.DbModel;
using Data.DbModel.BaseTypes;
using Microsoft.EntityFrameworkCore;

namespace Invoicify.Server;

public class InvoicifyDbContext : DbContext{
	public InvoicifyDbContext(DbContextOptions<InvoicifyDbContext> options) : base(options) { }
	
	public DbSet<Invoice> Invoice { get; set; }
	public DbSet<PartyInfo> PartyInfo { get; set; }
	public DbSet<Address> Address { get; set; }
	public DbSet<BankInfo> BankInfo { get; set; }
	public DbSet<OrderInfo> OrderInfo { get; set; }
	public DbSet<InvoiceItem> InvoiceItem { get; set; }


	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
		ModifyUpdate();
		return await base.SaveChangesAsync(cancellationToken);
	}

	public override int SaveChanges() {
		ModifyUpdate();
		return base.SaveChanges();
	}

	private void ModifyUpdate() {
		foreach (var entry in ChangeTracker.Entries<TimeStampedEntity>()) {
			if(entry.State == EntityState.Modified)
				entry.Entity.UpdatedAt = DateTimeOffset.Now;
		}
		
		UpdateQrCodeSpr();
	}

	private void UpdateQrCodeSpr() {
		foreach (var entry in ChangeTracker.Entries<Invoice>()) {
			if (entry.State is not (EntityState.Modified or EntityState.Added)) continue;
			var invoice = entry.Entity;
			invoice.QrSpr = invoice.QrPayment?.GetSpr();
		}
	}
	
}