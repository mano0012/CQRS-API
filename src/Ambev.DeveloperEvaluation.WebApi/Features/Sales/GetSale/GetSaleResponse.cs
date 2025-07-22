using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleResponse
{
    public Guid Id { get; private set; } = Guid.Empty;
    public Guid CustomerId { get; private set; } = Guid.Empty;
    public decimal TotalPrice { get; private set; } = 0;
    public string BranchName { get; private set; } = string.Empty;
    public List<GetSaleItemResponse> Items { get; private set; } = new();
    public Boolean Cancelled { get; private set; }
}