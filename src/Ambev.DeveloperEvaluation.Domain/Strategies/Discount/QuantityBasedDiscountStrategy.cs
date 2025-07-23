using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Strategies.Discount;

public class QuantityBasedDiscountStrategy : IDiscountStrategy
{
    public decimal CalculateDiscount(SaleItem item)
    {
        //Less than 4: No discount
        if (item.Quantity < 4)
            return 0;

        //Between 4 and 10: 10% discount
        if (item.Quantity < 10)
            return item.TotalPrice * 0.1m;

        //Between 10-20: 20% discount
        return item.TotalPrice * 0.2m;
    }
}
