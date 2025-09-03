namespace Data.DbModel;

public static partial class InvoiceExtensions {
	/// <summary>
	/// Gets the current date as DateOnly.
	/// </summary>
	public static DateOnly NOW => DateOnly.FromDateTime(DateTime.Now);

	/// <summary>
	/// Gets the due date by adding the specified number of days to the current date.
	/// </summary>
	/// <param name="days">Number of days to add</param>
	/// <returns>Due date as DateOnly</returns>
	public static DateOnly DUE(int days) => NOW.AddDays(days);

	/// <summary>
	/// Returns a test invoice with sample data (domestic bank account).
	/// </summary>
	/// <returns>Sample Invoice instance</returns>
	public static Invoice GetTestInvoice() {
		return new Invoice {
            Number = "0001124",
            Currency = "CZK",
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
	            Domestic = true,
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
	
	/// <summary>
	/// Returns a test invoice with sample data (IBAN and BIC for international payments).
	/// </summary>
	/// <returns>Sample Invoice instance</returns>
	public static Invoice GetTestIbanInvoice() {
		return new Invoice {
            Number = "0001124",
            Currency = "CZK",
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
	            Domestic = false,
	            Iban = "CZ0506000000000269816192",
	            Bic = "AGBACZPP"
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