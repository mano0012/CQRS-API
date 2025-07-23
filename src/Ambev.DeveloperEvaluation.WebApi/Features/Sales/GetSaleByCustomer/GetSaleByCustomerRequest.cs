namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleByCustomer;

public class GetSaleByCustomerRequest
{
    public Guid CustomerId { get; set; } = Guid.Empty;

}
