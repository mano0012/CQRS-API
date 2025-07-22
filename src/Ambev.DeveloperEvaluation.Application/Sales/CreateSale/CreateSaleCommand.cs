using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public string BranchName { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public List<CreateSaleItemCommand> Items { get; set; } = new();

    public void UpdateCustomerId(Guid CustomerId)
    {
        this.CustomerId = CustomerId;
    }
}
