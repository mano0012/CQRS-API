namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdResult
{
    public Guid Id { get; private set; } = Guid.Empty;
    public Guid CustomerId { get; private set; } = Guid.Empty;
    public string BranchName { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public IEnumerable<GetSaleItemByIdResult> Items { get; private set; } = new List<GetSaleItemByIdResult>();
    public Boolean Cancelled { get; private set; } = false;
    public decimal TotalPrice { get; private set; } = 0;
}
