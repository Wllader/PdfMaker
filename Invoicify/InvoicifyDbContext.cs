using Microsoft.EntityFrameworkCore;

namespace Invoicify;

public class InvoicifyDbContext : DbContext {
	public DbSet<Data.Invoice> Invoices { get; set; }
	public DbSet<Data.PartyInfo> Parties { get; set; }
	public DbSet<Data.BankInfo> Banks { get; set; }
	public DbSet<Data.OrderInfo> Orders { get; set; }
	public DbSet<Data.InvoiceItem> Items { get; set; }
	public DbSet<Data.QRPayment> QrPayments { get; set; }
	
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		optionsBuilder.UseSqlite("Data Source=Invoicify.db");
	}
}