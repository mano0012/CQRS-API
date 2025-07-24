using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleModifiedEvent : BaseEvent
{
    public Guid SaleId { get; }

    public SaleModifiedEvent(Guid saleId)
    {
        SaleId = saleId;
    }

    public override string GetEventMessage()
    {
        return $"Sale {SaleId} modified";
    }
}
