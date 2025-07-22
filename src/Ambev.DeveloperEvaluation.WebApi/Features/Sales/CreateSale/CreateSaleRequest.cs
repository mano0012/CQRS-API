namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public string BranchName { get; private set; } = string.Empty;
    public List<CreateSaleItemRequest> Items { get; private set; } = new();
}