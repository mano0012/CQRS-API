namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;

public class GetSaleByIdResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid CustomerId { get; set; } = Guid.Empty;
    public decimal TotalPrice { get; set; } = 0;
    public string BranchName { get; set; } = string.Empty;
    public IEnumerable<GetSaleItemByIdResponse> Items { get; set; } = new List<GetSaleItemByIdResponse>();
    public Boolean Cancelled { get; set; }
    public DateTime CreatedAt { get; set; }
}