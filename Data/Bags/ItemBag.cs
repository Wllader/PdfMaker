using Data.Bags.BaseTypes;

namespace Data.Bags;

/// <summary>
/// Represents an item for data transfer or storage.
/// </summary>
public class ItemBag : TimeStampedBag {
	public string Name { get; set; }
	public string Description { get; set; }
	public string Unit { get; set; }
	
	public string OrderNumber { get; set; }
}