using Data.Bags.BaseTypes;

namespace Data.Bags;

public class OrderBag : TimeStampedBag {
	public string Number { get; set; }
	public DateOnly Date { get; set; }
	public string Delivery { get; set; }
}