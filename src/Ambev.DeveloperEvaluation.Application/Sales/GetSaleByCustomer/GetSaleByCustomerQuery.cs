using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByCustomer;

public class GetSaleByCustomerQuery : IRequest<List<GetSaleByCustomerResult>>
{
    public Guid CustomerId { get; set; }

    public GetSaleByCustomerQuery(Guid customerId)
    {
        CustomerId = customerId;
    }
}