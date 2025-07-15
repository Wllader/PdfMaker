using Data.Bags.BaseTypes;

namespace Data.Bags;

public class InvoiceBag : TimeStampedBag {
	public string? Number { get; set; }

	public string SellerName { get; set; }
	public string CustomerName { get; set; }

	public DateOnly IssueDate { get; set; }
	public DateOnly DueDate { get; set; }
}
