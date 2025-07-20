namespace Ambev.DeveloperEvaluation.WebApi.Features.Items.CreateItem;

public class GetItemRequest
{
    /// <summary>
    /// Gets or sets the item name. Must be unique and contain only valid characters.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the item price. Must be a value bigger than zero.
    /// </summary>
    public decimal Price { get; set; } = 0;
}