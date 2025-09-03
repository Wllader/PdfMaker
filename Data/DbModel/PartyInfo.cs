using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Data.DbModel.BaseTypes;
using Microsoft.EntityFrameworkCore;

namespace Data.DbModel;

/// <summary>
/// Represents a party (person or company) involved in an invoice, including address, VAT ID, and invoice relations.
/// </summary>
[Index(nameof(VatId))]
[Index(nameof(LastName), nameof(FirstName), AllDescending = true)]
public class PartyInfo : TimeStampedEntity {
	public string FirstName { get; set; }
	public string LastName { get; set; }

	public string FullName => $"{FirstName} {LastName}";

	public Address Address { get; set; } = new();
	public string VatId { get; set; }
	
	[EmailAddress]
	public string? Email { get; set; }
	
	[Phone]
	public string? Phone { get; set; }
	
	/// <summary>
	/// Invoices where this party is the seller.
	/// </summary>
	[JsonIgnore]
	public List<Invoice> InvoiceAsSeller { get; set; } = [];
	/// <summary>
	/// Invoices where this party is the customer.
	/// </summary>
	[JsonIgnore]
	public List<Invoice> InvoiceAsCustomer { get; set; } = [];
}