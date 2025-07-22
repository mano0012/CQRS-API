using Ambev.DeveloperEvaluation.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Messaging;

public class DomainEventPublisher : IDomainEventPublisher
{
    public Task PublishAsync(object domainEvent)
    {
        Console.WriteLine($"Domain Event Published: {domainEvent.GetType().Name}");
        return Task.CompletedTask;
    }
}
