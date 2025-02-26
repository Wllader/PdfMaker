using System.ComponentModel.DataAnnotations;

namespace Data;

public class PartyInfo {
	[Key]
	public Guid Id { get; set; } = Guid.CreateVersion7();
	
	public string Name { get; set; }
	public Address Address { get; set; }
	public string VatId { get; set; }
	
	[EmailAddress]
	public string? Email { get; set; }
	
	[Phone]
	public string? Phone { get; set; }
}