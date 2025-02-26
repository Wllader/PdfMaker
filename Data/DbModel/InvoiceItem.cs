using System.ComponentModel.DataAnnotations;

namespace Data;

public class InvoiceItem {
	[Key]
	public Guid Id { get; set; } = Guid.CreateVersion7();
	public Guid InvoiceId { get; set; }
	public Invoice Invoice { get; set; }

	public string? Name { get; set; }
	public string? Description { get; set; }
	
	public string Unit { get; set; }
	public decimal Quantity { get; set; }
	public decimal PricePerUnit { get; set; }
	public decimal TotalPrice => Quantity * PricePerUnit;
}