namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public DateTime Date { get; set; }
    public string BranchName { get; set; } = string.Empty;
    //public List<SaleItemRequest> Items { get; set; } = new();
}

