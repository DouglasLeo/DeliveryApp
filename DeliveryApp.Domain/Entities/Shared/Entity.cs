namespace DeliveryApp.Domain.Entities.Shared;

public abstract class Entity : IEquatable<Guid>
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; protected set; }

    public bool Equals(Guid id) => id == Id;
}