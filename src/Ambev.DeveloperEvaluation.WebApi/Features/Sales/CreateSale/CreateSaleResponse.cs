using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleResponse
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string BranchName { get; private set; } = string.Empty;
    public Guid CustomerId { get; private set; }
    public List<CreateSaleItemResponse> Items { get; private set; } = new();

}
