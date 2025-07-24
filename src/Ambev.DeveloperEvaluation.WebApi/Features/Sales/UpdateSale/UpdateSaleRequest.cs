namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequest
{
    public Guid SaleId { get; set; } = Guid.Empty;
    public Guid CustomerId { get; set; } = Guid.Empty;
    public string BranchName { get; set; } = string.Empty;
}
