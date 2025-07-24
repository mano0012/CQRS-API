namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByCustomer;

public class GetSaleByCustomerResult
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid CustomerId { get; set; } = Guid.Empty;
    public string BranchName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<GetSaleItemByCustomerResult> Items { get; set; } = new List<GetSaleItemByCustomerResult>();
    public Boolean Cancelled { get; set; } = false;
    public decimal TotalPrice { get; set; } = 0;
}
