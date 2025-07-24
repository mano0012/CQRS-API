namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSaleItem;

public class DeleteSaleItemResponse
{
    public Guid ItemId { get; set; } = Guid.Empty;
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
    public bool Cancelled { get; set; } = false;
    public decimal UnitPrice { get; set; } = 0;
    public decimal Discount { get; set; } = 0;
    public decimal TotalPrice { get; set; } = 0;
}