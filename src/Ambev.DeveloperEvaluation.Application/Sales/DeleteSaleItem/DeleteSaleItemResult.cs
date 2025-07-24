using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSaleItem;

public class DeleteSaleItemResult
{
    public Guid ItemId { get; private set; } = Guid.Empty;
    public string ItemName { get; private set; } = string.Empty;
    public int Quantity { get; private set; } = 0;
    public bool Cancelled { get; private set; } = false;
    public decimal UnitPrice { get; private set; } = 0;
    public decimal Discount { get; private set; } = 0;

    public decimal TotalPrice = 0;

    public decimal TotalPriceWithDiscount = 0;
}
