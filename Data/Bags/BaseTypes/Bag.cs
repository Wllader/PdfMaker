namespace Data.Bags.BaseTypes;

public abstract class Bag {
	public Guid Id { get; init; }

	public override bool Equals(object? obj) => obj is Bag other && other.Id == Id;
	public override int GetHashCode() => Id.GetHashCode();
}

public abstract class TimeStampedBag : Bag {
	public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }
}