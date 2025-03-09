using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Data.DbModel.BaseTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbModel;

public class InvoiceItem : TimeStampedEntity {
	public Guid InvoiceId { get; set; }
	[JsonIgnore]
	public Invoice Invoice { get; set; }

	public Guid? OrderInfoId { get; set; }
	[JsonIgnore]
	public OrderInfo? OrderInfo { get; set; }
	
	public string? Name { get; set; }
	public string? Description { get; set; }
	
	public string Unit { get; set; }
	public decimal Quantity { get; set; }
	public decimal PricePerUnit { get; set; }
	public decimal TotalPrice => Quantity * PricePerUnit;
}