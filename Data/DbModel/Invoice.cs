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

	[NotMapped]	
	public QrPayment? QrPayment { get; set; }
	
	[MaxLength(512)]
	public string? QrSpr { get; set; }
	
	public static Invoice GetTestInvoice() {
		return new Invoice {
            Number = "0001124",
            QrPayment = new QrPayment {
	            Account = "CZ0506000000000269816192+AGBACZPP",
	            Amount = 147.12m,
	            Currency = "CZK",
	            RecipientName = "Vladimír Drechsler",
	            MessageForRecipient = "Test",
	            VariableSymbol = 135
            },
            SellerInfo = new PartyInfo {
               	FirstName = "Aleš",
                LastName = "Ondráček",
               	Address = new Address {
	                Street = "Pražská 92, Ondřejovice",
	                ZipCode = "331 05",
	                City = "Ondřejovice",
	                Country = "Česká republika",
	                State = "Středočeský kraj",
                },
               	VatId = "CZ123456789",
                Email = "ales.ondracek@ondracek.eu",
                Phone = "+420 510 045 555"
            },
            BankInfo = new BankInfo {
	            Name = "Moneta Money Bank",
	            Account = "000000000000/0600",
            },
            VariableSymbol = "0001124",
            CustomerInfo = new PartyInfo {
               	FirstName = "Jan",
                LastName = "Novák",
               	Address = new Address {
	                Street = "Plzeňská 43/50",
	                ZipCode = "101 00",
	                City = "Praha",
	                Country = "Česká republika",
                },
               	VatId = "CZ123456789",
                Email = "this.is.just@a.test.com",
                Phone = "+420 456 789 000"
            },
            DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(14)),
            IssueDate = DateOnly.FromDateTime(DateTime.Now.Subtract(TimeSpan.FromDays(2))),
            Items = [
               	new InvoiceItem {
               		Name = "Faktura",
               		Description = "Chtěl bych jen vědět, co se stane, když tady bude opravdu, ale vážně opravdu dlouhý popis.",
               		Unit = "ks",
               		PricePerUnit = 250,
               		Quantity = 4,
               	},
           
               	new InvoiceItem {
               		Name = "PDF",
               		Description = "Generování PDF",
               		Unit = "ks",
               		PricePerUnit = 100,
               		Quantity = 12,
               	}
            ],
            OrdersInfo = [
	            new OrderInfo {
		            Number = "0001124",
		            Date = DateOnly.FromDateTime(DateTime.Now.Subtract(TimeSpan.FromDays(2))),
		            Delivery = "E-mail",
	            },
	            new OrderInfo {
		            Number = "0001125",
		            Date = DateOnly.FromDateTime(DateTime.Now.Subtract(TimeSpan.FromDays(4))),
		            Delivery = "PPL",
	            }
            ],
		};
	}
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