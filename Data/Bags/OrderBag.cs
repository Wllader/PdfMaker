using Data.Bags.BaseTypes;

namespace Data.Bags;

/// <summary>
/// Represents order information for data transfer or storage.
/// </summary>
public class OrderBag : TimeStampedBag {
	public string Number { get; set; }
	public DateOnly Date { get; set; }
	public string Delivery { get; set; }
}