using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public List<CreateSaleItemResponse> Items { get; set; } = new();

}
