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
        Console.WriteLine($"{domainEvent.GetType().Name}: {((BaseEvent)domainEvent).GetEventMessage()}");
        return Task.CompletedTask;
    }
}
