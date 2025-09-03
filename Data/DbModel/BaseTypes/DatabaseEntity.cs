using System.ComponentModel.DataAnnotations;

namespace Data.DbModel.BaseTypes;

/// <summary>
/// Base class for database entities with unique identifier.
/// </summary>
public abstract class DatabaseEntity {
	[Key]
	public Guid Id { get; init; } = Guid.CreateVersion7();

	public override bool Equals(object? obj) => obj is DatabaseEntity other && other.Id == Id;
	public override int GetHashCode() => Id.GetHashCode();
}

/// <summary>
/// Base class for entities with creation and update timestamps.
/// </summary>
public abstract class TimeStampedEntity : DatabaseEntity {
	public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
	public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}