using System.ComponentModel.DataAnnotations;

namespace Data;

public class Address {
	[Key]
	public Guid Id { get; set; } = Guid.CreateVersion7();
	
	public string Street { get; set; }
	public string City { get; set; }
	public string ZipCode { get; set; }
	public string Country { get; set; }
	public string? State { get; set; }
}