namespace Data.DbModel;

public static partial class InvoiceExtensions {
	public static DateOnly NOW => DateOnly.FromDateTime(DateTime.Now);
	public static DateOnly DUE(int days) => NOW.AddDays(days);
	
	public static Invoice GetTestInvoice() {
		return new Invoice {
            Number = "0001124",
            Currency = "CZK",
            QrPayment = new QrPayment {
	            // Account = "CZ0506000000000269816192+AGBACZPP",
	            // Account = "CZ0506000000000269816192",
	            Account = "269816192",
	            BankNumber = "0600",
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
	            Account = "000000000000",
	            BankNumber = "1111"
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