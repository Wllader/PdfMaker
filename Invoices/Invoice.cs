using System.ComponentModel.DataAnnotations;

namespace PDFMaker.Invoices;

public record struct Invoice {
	public string Number { get; set; }
	public string? VariableSymbol { get; set; }
	
	public DateOnly IssueDate { get; set; }
	public DateOnly DueDate { get; set; }
	
	public PartyInfo SellerInfo { get; set; }
	public PartyInfo CustomerInfo { get; set; }
	
	public BankInfo BankInfo { get; set; }
	
	public List<OrderInfo> OrdersInfo { get; set; }
	
	public List<InvoiceItem> Items { get; set; }

	public static Invoice GetTestInvoice() {
		return new Invoice {
            Number = "0001124",
            SellerInfo = new PartyInfo {
               	Name = "Vladimír Drechsler",
               	Address = new Address {
	                Street = "M. Alše 795",
	                ZipCode = "262 52",
	                City = "Horoměřice",
	                Country = "Česká republika",
	                State = "Středočeský kraj",
                },
               	VatId = "CZ123456789",
                Email = "vladimir.drechsler@wllader.cz",
                Phone = "+420 734 210 041"
            },
            BankInfo = new BankInfo {
	            Name = "Moneta Money Bank",
	            Account = "000000000000/0600",
	            QRCode = new QRCode {
		            Account = "CZ0506000000000269816192+AGBACZPP",
		            Amount = 147.12m,
		            Currency = "CZK",
		            RecipientName = "Vladimír Drechsler",
		            MessageForRecipient = "Test",
		            VariableSymbol = 135
	            }
            },
            VariableSymbol = "0001124",
            CustomerInfo = new PartyInfo {
               	Name = "Jan Novák",
               	Address = new Address {
	                Street = "Petrohradská 92/11",
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
            ]
		};
	}
}

public record struct PartyInfo {
	public string Name { get; set; }
	public Address Address { get; set; }
	public string VatId { get; set; }
	
	[EmailAddress]
	public string? Email { get; set; }
	
	[Phone]
	public string? Phone { get; set; }
}

public record struct Address {
	public string Street { get; set; }
	public string City { get; set; }
	public string ZipCode { get; set; }
	public string Country { get; set; }
	public string? State { get; set; }
}

public record struct BankInfo {
	public string Name { get; set; }
	public string Account { get; set; }
	
	public QRCode? QRCode { get; set; } 
}

public record struct OrderInfo {
	public string Number { get; set; }
	public DateOnly Date { get; set; }
	public string? Delivery { get; set; }
}

public record struct InvoiceItem {
	public string? Name { get; set; }
	public string? Description { get; set; }
	
	public string Unit { get; set; }
	public decimal Quantity { get; set; }
	public decimal PricePerUnit { get; set; }
	public decimal TotalPrice => Quantity * PricePerUnit;
}