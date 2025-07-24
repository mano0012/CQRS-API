using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSaleItem;

public class DeleteSaleItemCommand : IRequest<DeleteSaleItemResult>
{
    public Guid SaleId { get; set; } = Guid.Empty;
    public Guid ItemId { get; set; } = Guid.Empty;
}
