namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;

public class GetSaleByIdRequest
{
    public Guid SaleId { get; set; } = Guid.Empty;
}