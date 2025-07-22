namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleItemCommand
{
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }
}