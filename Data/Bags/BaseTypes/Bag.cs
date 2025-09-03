namespace Data.Bags.BaseTypes;

/// <summary>
/// Base class for data transfer objects (bags).
/// </summary>
public abstract class Bag {
	public Guid Id { get; init; }

	public override bool Equals(object? obj) => obj is Bag other && other.Id == Id;
	public override int GetHashCode() => Id.GetHashCode();
}

/// <summary>
/// Base class for bags with creation and update timestamps.
/// </summary>
public abstract class TimeStampedBag : Bag {
	public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }
}