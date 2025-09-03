using Data.Bags.BaseTypes;

namespace Data.Bags;

/// <summary>
/// Represents a party (person or company) for data transfer or storage.
/// </summary>
public class PartyBag : TimeStampedBag {
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string VatId { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
}