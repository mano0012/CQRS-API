namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;

public class GetSaleItemByIdResponse
{
    public Guid ItemId { get; set; } = Guid.Empty;
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
    public bool Cancelled { get; set; } = false;
    public decimal UnitPrice { get; set; } = 0;
    public decimal Discount { get; set; } = 0;
    public decimal TotalPrice { get; set; } = 0;
}