using Bogus;
using Data.DbModel;

namespace FakeData;
public static class FakeData {
	private static readonly Faker Faker = new("cz");
	
	public static Invoice RandomInvoice() {
		var no = Faker.Random.ReplaceNumbers("8######");
		var monthago = DateOnly.FromDateTime(DateTime.Today.AddMonths(-1));
		var inamonth = DateOnly.FromDateTime(DateTime.Today.AddMonths(1));
		
		var issue = Faker.Date.BetweenDateOnly(monthago, inamonth);
		
		return new Invoice {
			Number = no,
			SellerInfo = RandomPartyInfo(),
			CustomerInfo = RandomPartyInfo(),
			Currency = "CZK",
			BankInfo = RandomBankInfo(),
			VariableSymbol = no,
			IssueDate = issue,
			DueDate = Faker.Date.SoonDateOnly(60, issue),
			Items = Faker.Make(3, RandomInvoiceItem).ToList(),
			OrdersInfo = Faker.Make(2, RandomOrderInfo).ToList(),
		};
	}
	
	public static PartyInfo RandomPartyInfo() {
		bool company = Faker.Random.Bool(0.2f);
		
		return new PartyInfo {
			FirstName = company ? Faker.Company.CompanyName() : Faker.Name.FirstName(),
			LastName = company ? string.Empty : Faker.Name.LastName(),
			VatId = Faker.Random.ReplaceNumbers("CZ#########"),
			Email = Faker.Internet.Email(),
			Phone = Faker.Phone.PhoneNumber("+420 #########"),
			Address = RandomAddress()
		};
	}
	
	public static Address RandomAddress() {
		return new Address {
			Street = Faker.Address.StreetAddress(),
			City = Faker.Address.City(),
			ZipCode = Faker.Address.ZipCode(),
			Country = Faker.Address.Country(),
			State = Faker.Address.State(),
		};
	}
	
	public static BankInfo RandomBankInfo() {
		bool domestic = Faker.Random.Bool(0.8f);
		
		var bi = new BankInfo {
			Name = Faker.Company.CompanyName(),
			Domestic = domestic,
		};

		if (domestic) {
			bi.Account = Faker.Finance.Account();
			bi.BankNumber = Faker.Random.ReplaceNumbers("####");
		} else {
			bi.Iban = Faker.Finance.Iban();
			bi.Bic = Faker.Finance.Bic();
		}
		
		return bi;
	}
	
	public static InvoiceItem RandomInvoiceItem() {
		return new InvoiceItem {
			Name = Faker.Commerce.ProductName(),
			Description = Faker.Lorem.Sentence(10),
			Unit = Faker.Random.Replace("??").ToLower(),
			PricePerUnit = Faker.Finance.Amount(1, 1000, 2),
			Quantity = Faker.Finance.Amount(1, 1000, 2)
		};
	}
	
	public static OrderInfo RandomOrderInfo() {
		return new OrderInfo {
			Number = Faker.Random.ReplaceNumbers("8#######"),
			Date = Faker.Date.PastDateOnly(2, DateOnly.FromDateTime(DateTime.Today)),
			Delivery = Faker.Lorem.Word()
		};
	}
	
}