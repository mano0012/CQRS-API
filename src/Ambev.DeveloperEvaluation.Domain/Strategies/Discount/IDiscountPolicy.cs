using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Strategies.Discount;

public interface IDiscountStrategy
{
    decimal CalculateDiscount(SaleItem item);
}
