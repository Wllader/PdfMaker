using System.Text.Json.Serialization;
using Data.DbModel.BaseTypes;
using Microsoft.EntityFrameworkCore;

namespace Data.DbModel;

[Index(nameof(Account))]
public class BankInfo : TimeStampedEntity {
	public Guid InvoiceId { get; set; }
	[JsonIgnore]
	public Invoice Invoice { get; set; }
	
	public string Name { get; set; }
	public string? Account { get; set; }
	public string? Iban { get; set; }
	public string? BankNumber { get; set; }
	public string? Bic { get; set; }
	
	public bool Domestic { get; set; }
}