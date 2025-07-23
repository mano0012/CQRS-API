using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdQuery : IRequest<GetSaleByIdResult>
{
    public Guid SaleId { get; set; }
    public Guid CustomerId { get; set; }



    public GetSaleByIdQuery()
    {}

    public void UpdateCustomerId(Guid customerId)
    {
        CustomerId = customerId;
    }
}
