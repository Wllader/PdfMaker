using Data.Bags.BaseTypes;

namespace Data.Bags;

/// <summary>
/// Represents basic invoice data for transfer or storage.
/// </summary>
public class InvoiceBag : TimeStampedBag {
	public string? Number { get; set; }

	public string SellerName { get; set; }
	public string CustomerName { get; set; }

	public DateOnly IssueDate { get; set; }
	public DateOnly DueDate { get; set; }
}
