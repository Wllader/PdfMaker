using Data.Bags.BaseTypes;

namespace Data.Bags;

public class ItemBag : TimeStampedBag {
	public string Name { get; set; }
	public string Description { get; set; }
	public string Unit { get; set; }
	
	public string OrderNumber { get; set; }
}