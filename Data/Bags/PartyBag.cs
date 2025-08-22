using Data.Bags.BaseTypes;

namespace Data.Bags;

public class PartyBag : TimeStampedBag {
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string VatId { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
}