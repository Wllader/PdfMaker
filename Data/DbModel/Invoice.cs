using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Data.DbModel.BaseTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbModel;

[Index(nameof(Number))]
[EntityTypeConfiguration(typeof(InvoiceConfiguration))]
public class Invoice : TimeStampedEntity {
	[MaxLength(64)]
	[RegularExpression("[0-9]*")]
	public string? Number { get; set; }
	
	[MaxLength(10)]
	[RegularExpression("\\d*")]
	public string? VariableSymbol { get; set; }

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

	public decimal TotalPrice => Items.Sum(i => i.TotalPrice);

	[NotMapped]	
	public QrPayment? QrPayment { get; set; }
	
	[MaxLength(512)]
	public string? QrSpr { get; set; }
	
	
}

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice> {
	public void Configure(EntityTypeBuilder<Invoice> builder) {
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

