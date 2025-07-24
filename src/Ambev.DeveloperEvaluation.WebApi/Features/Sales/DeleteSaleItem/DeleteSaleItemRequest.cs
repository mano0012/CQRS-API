namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSaleItem;

public class DeleteSaleItemRequest
{
    public Guid ItemId { get; set; } = Guid.Empty;
    public Guid SaleId { get; set; } = Guid.Empty;
}
