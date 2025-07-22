namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleItemRequest
{
    public Guid ItemId { get; private set; } = Guid.Empty;
    public int Quantity { get; private set; } = 0;
}
