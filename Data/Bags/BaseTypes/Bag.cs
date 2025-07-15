namespace Data.Bags.BaseTypes;

public abstract class Bag {
	public Guid Id { get; set; }
}

public abstract class TimeStampedBag : Bag {
	public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }
}