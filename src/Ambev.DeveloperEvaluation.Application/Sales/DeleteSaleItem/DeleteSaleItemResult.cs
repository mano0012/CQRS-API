using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSaleItem;

public class DeleteSaleItemResult
{
    public Guid ItemId { get; set; } = Guid.Empty;
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
    public bool Cancelled { get; set; } = false;
    public decimal UnitPrice { get; set; } = 0;
    public decimal Discount { get; set; } = 0;

    public decimal TotalPrice = 0;

    public decimal TotalPriceWithDiscount = 0;
}
