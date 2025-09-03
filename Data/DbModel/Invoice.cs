using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Data.DbModel.BaseTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbModel;

/// <summary>
/// Represents an invoice, including parties, items, bank info, and payment details.
/// </summary>
[Index(nameof(Number))]
[EntityTypeConfiguration(typeof(InvoiceConfiguration))]
public class Invoice : TimeStampedEntity {
	[MaxLength(64)]
	[RegularExpression("[0-9]*")]
	public string? Number { get; set; }
	
	[MaxLength(10)]
	[RegularExpression("\\d*")]
	public string? VariableSymbol { get; set; }
	
	[Length(3, 3)]
	public string? Currency { get; set; }

	public required DateOnly IssueDate { get; set; }
	public required DateOnly DueDate { get; set; }
	
	[JsonIgnore]
	public Guid SellerInfoId { get; set; }
	public required PartyInfo SellerInfo { get; set; }
	
	[JsonIgnore]
	public Guid CustomerInfoId { get; set; }
	public required PartyInfo CustomerInfo { get; set; }
	

	public required BankInfo BankInfo { get; set; }
	

	public List<OrderInfo> OrdersInfo { get; set; } = [];
	public List<InvoiceItem> Items { get; set; } = [];

	/// <summary>
	/// Gets the total price of all invoice items.
	/// </summary>
	public decimal TotalPrice => Items.Sum(i => i.TotalPrice);

	[NotMapped]
	public QrPayment? QrPayment { get; set; }
	
	[MaxLength(512)]
	public string? QrSpr { get; set; }
	
	
}

/// <summary>
/// Entity Framework configuration for Invoice relationships.
/// </summary>
public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice> {
	/// <summary>
	/// Configures the Invoice entity relationships for Entity Framework.
	/// </summary>
	/// <param name="builder">EntityTypeBuilder for Invoice</param>
	public void Configure(EntityTypeBuilder<Invoice> builder) {
		// SellerInfo and CustomerInfo are set to Restrict delete to avoid accidental cascade deletes.
		builder
			.HasOne<PartyInfo>(i => i.SellerInfo)
			.WithMany(si => si.InvoiceAsSeller)
			.HasForeignKey(i => i.SellerInfoId)
			.OnDelete(DeleteBehavior.Restrict);
		
		builder
			.HasOne<PartyInfo>(i => i.CustomerInfo)
			.WithMany(si => si.InvoiceAsCustomer)
			.HasForeignKey(i => i.CustomerInfoId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
