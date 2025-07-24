using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid CustomerId { get; set; } = Guid.Empty;
    public string BranchName { get; set; } = string.Empty;
}
