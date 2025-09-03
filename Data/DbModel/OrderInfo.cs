using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Data.DbModel.BaseTypes;
using Microsoft.EntityFrameworkCore;

namespace Data.DbModel;

/// <summary>
/// Represents order information related to an invoice.
/// </summary>
[Index(nameof(Number))]
[Index(nameof(Date), AllDescending = true)]
public class OrderInfo : TimeStampedEntity {
	public Guid InvoiceId { get; set; }
	[JsonIgnore]
	public Invoice Invoice { get; set; }

	public string Number { get; set; }
	public DateOnly Date { get; set; }
	public string? Delivery { get; set; }
}