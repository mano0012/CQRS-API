namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByCustomer;

public class GetSaleByCustomerResult
{
    public Guid Id { get; private set; } = Guid.Empty;
    public Guid CustomerId { get; private set; } = Guid.Empty;
    public string BranchName { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<GetSaleItemByCustomerResult> Items { get; private set; } = new List<GetSaleItemByCustomerResult>();
    public Boolean Cancelled { get; private set; } = false;
    public decimal TotalPrice { get; private set; } = 0;
}
