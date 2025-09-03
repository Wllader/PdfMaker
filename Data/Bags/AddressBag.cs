using Data.Bags.BaseTypes;

namespace Data.Bags;

/// <summary>
/// Represents a postal address for data transfer or storage.
/// </summary>
public class AddressBag : TimeStampedBag {
	public string Street { get; set; }
	public string City { get; set; }
	public string ZipCode { get; set; }
	public string Country { get; set; }
	public string? State { get; set; }
}