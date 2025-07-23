namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleItemRequest
{
    public Guid ItemId { get; set; } = Guid.Empty;
    public int Quantity { get; set; } = 0;
}
