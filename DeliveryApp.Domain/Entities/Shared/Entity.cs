namespace DeliveryApp.Domain.Entities.Shared;

public abstract class Entity : IEquatable<Guid>
{
    public Guid Id { get; protected set; } = Guid.CreateVersion7();
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; protected set; }

    public bool Equals(Guid id) => id == Id;
}