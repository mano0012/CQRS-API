namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleByCustomer;

public class GetSaleByCustomerResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid CustomerId { get; set; } = Guid.Empty;
    public decimal TotalPrice { get; set; } = 0;
    public string BranchName { get; set; } = string.Empty;
    public IEnumerable<GetSaleItemByCustomerResponse> Items { get; set; } = new List<GetSaleItemByCustomerResponse>();
    public Boolean Cancelled { get; set; }
    public DateTime CreatedAt { get; set; }
}