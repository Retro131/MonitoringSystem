namespace Models.Entities;

public class Device(Guid id)
{
    public Guid Id { get; init; } = id;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime LastSeenAt { get; set; } = DateTime.UtcNow;

    public List<Session> Sessions { get; set; } = [];
}