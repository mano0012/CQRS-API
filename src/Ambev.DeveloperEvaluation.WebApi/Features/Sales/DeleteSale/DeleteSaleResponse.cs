namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

public class DeleteSaleResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid CustomerId { get; set; } = Guid.Empty;
    public decimal TotalPrice { get; set; } = 0;
    public string BranchName { get; set; } = string.Empty;
    public IEnumerable<DeleteSaleItemsResponse> Items { get; set; } = new List<DeleteSaleItemsResponse>();
    public Boolean Cancelled { get; set; }
    public DateTime CreatedAt { get; set; }
}