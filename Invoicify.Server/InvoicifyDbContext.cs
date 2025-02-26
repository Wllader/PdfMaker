using Data;
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
}