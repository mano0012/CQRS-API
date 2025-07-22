namespace Ambev.DeveloperEvaluation.Domain.Events;

public interface IDomainEventPublisher
{
    Task PublishAsync(object domainEvent);
}
