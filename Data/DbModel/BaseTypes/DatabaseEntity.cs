using System.ComponentModel.DataAnnotations;

namespace Data.DbModel.BaseTypes;

public abstract class DatabaseEntity {
	[Key]
	public Guid Id { get; init; } = Guid.CreateVersion7();
}

public abstract class TimeStampedEntity : DatabaseEntity {
	public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;
	public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
}