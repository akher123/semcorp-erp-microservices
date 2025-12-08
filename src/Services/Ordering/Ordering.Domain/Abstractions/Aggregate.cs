namespace Ordering.Domain.Abstractions;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent dom)
    {
        _domainEvents.Add(dom);
    }
    public IDomainEvent[] ClearDomainEvents()
    {
        IDomainEvent[] domainEvents = _domainEvents.ToArray();

        _domainEvents.Clear();

        return domainEvents;

    }
}
