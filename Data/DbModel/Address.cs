using System.ComponentModel.DataAnnotations;
using Data.DbModel.BaseTypes;

namespace Data.DbModel;

public class Address : TimeStampedEntity {
	public string Street { get; set; }
	public string City { get; set; }
	public string ZipCode { get; set; }
	public string Country { get; set; }
	public string? State { get; set; }
}