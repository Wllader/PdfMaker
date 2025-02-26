using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data;

public class BankInfo {
	[Key]
	public Guid Id { get; set; } = Guid.CreateVersion7();
	public Guid InvoiceId { get; set; }
	public Invoice Invoice { get; set; }

	public string Name { get; set; }
	public string Account { get; set; }
	
	[NotMapped]
	public QRPayment? QrCode { get; set; }
}