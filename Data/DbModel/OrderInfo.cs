using System.ComponentModel.DataAnnotations;

namespace Data;

public class OrderInfo {
	[Key]
	public Guid Id { get; set; } = Guid.CreateVersion7();
	public Guid InvoiceId { get; set; }
	public Invoice Invoice { get; set; }

	public string Number { get; set; }
	public DateOnly Date { get; set; }
	public string? Delivery { get; set; }
}