using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleItemCancelledEvent : BaseEvent
{
    public Guid SaleItemId { get; }

    public SaleItemCancelledEvent(Guid saleItemId)
    {
        SaleItemId = saleItemId;
    }

    public override string GetEventMessage()
    {
        return $"Sale Item {SaleItemId} cancelled";
    }
}
