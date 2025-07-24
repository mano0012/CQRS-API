using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public Guid SaleId { get; set; }
    public Guid CustomerId { get; set; } = Guid.Empty;
    public string BranchName { get; set; } = string.Empty;

}
