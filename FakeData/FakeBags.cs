using Bogus;
using Data.Bags;


namespace FakeData;
public static class FakeBags {
	private static readonly Faker Faker = new("cz");

	/// <summary>
	/// Generates a random party bag (person or company).
	/// </summary>
	/// <returns>Random PartyBag instance</returns>
	public static PartyBag RandomPartyBag() {
		bool company = Faker.Random.Bool(0.2f);

		return new PartyBag {
			FirstName = company ? Faker.Company.CompanyName() : Faker.Name.FirstName(),
			LastName = company ? string.Empty : Faker.Name.LastName(),
			VatId = Faker.Random.ReplaceNumbers("CZ#########"),
			Email = Faker.Internet.Email(),
			Phone = Faker.Phone.PhoneNumber("+420 #########"),
		};
	}
	
	/// <summary>
	/// Generates a random address bag.
	/// </summary>
	/// <returns>Random AddressBag instance</returns>
	public static AddressBag RandomAddressBag() {
		return new AddressBag {
			Street = Faker.Address.StreetAddress(),
			City = Faker.Address.City(),
			ZipCode = Faker.Address.ZipCode(),
			Country = Faker.Address.Country(),
			State = Faker.Address.State(),
		};
	}
	
	/// <summary>
	/// Generates a random item bag.
	/// </summary>
	/// <returns>Random ItemBag instance</returns>
	public static ItemBag RandomItemBag() {
		return new ItemBag {
			Name = Faker.Commerce.ProductName(),
			Description = Faker.Lorem.Sentence(10),
			Unit = Faker.Commerce.ProductAdjective(),
		};
	}
	
	/// <summary>
	/// Generates a random order bag.
	/// </summary>
	/// <returns>Random OrderBag instance</returns>
	public static OrderBag RandomOrderBag() {
		return new OrderBag {
			Number = Faker.Random.ReplaceNumbers("8#######"),
			Date = Faker.Date.PastDateOnly(2, DateOnly.FromDateTime(DateTime.Today)),
			Delivery = Faker.Lorem.Word()
		};
	}
	
}