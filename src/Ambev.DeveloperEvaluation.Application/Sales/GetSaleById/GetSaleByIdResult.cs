namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdResult
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid CustomerId { get; set; } = Guid.Empty;
    public string BranchName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public IEnumerable<GetSaleItemByIdResult> Items { get; set; } = new List<GetSaleItemByIdResult>();
    public Boolean Cancelled { get; set; } = false;
    public decimal TotalPrice { get; set; } = 0;
}
