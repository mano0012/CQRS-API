using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCancelledEvent : BaseEvent
{
    public Guid SaleId { get; }
    public Guid CustomerId { get; }

    public SaleCancelledEvent(Guid saleId, Guid customerId)
    {
        SaleId = saleId;
        CustomerId = customerId;
    }

    public override string GetEventMessage()
    {
        return $"Sale {SaleId} from customer {CustomerId} cancelled";
    }
}
