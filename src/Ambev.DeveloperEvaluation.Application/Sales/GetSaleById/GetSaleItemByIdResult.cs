namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleItemByIdResult
{
    public Guid ItemId { get; private set; } = Guid.Empty;
    public string ItemName { get; private set; } = string.Empty;
    public int Quantity { get; private set; } = 0;
    public bool Cancelled { get; private set; } = false;
    public decimal UnitPrice { get; private set; } = 0;
    public decimal Discount { get; private set; } = 0;
    public decimal TotalPrice { get; private set; } = 0;
    public decimal TotalPriceWithDiscount { get; private set; } = 0;
}
