using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Data.DbModel.BaseTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbModel;

[Index(nameof(Account))]
public class BankInfo : TimeStampedEntity {
	public Guid InvoiceId { get; set; }
	[JsonIgnore]
	public Invoice Invoice { get; set; }

	public string Name { get; set; }
	public string Account { get; set; }
}