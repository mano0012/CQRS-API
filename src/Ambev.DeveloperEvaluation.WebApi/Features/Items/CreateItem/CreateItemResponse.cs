namespace Ambev.DeveloperEvaluation.WebApi.Features.Items.CreateItem;

public class CreateItemResponse
{
    /// <summary>
    /// The unique identifier of the created item
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets or sets the item name. Must be unique and contain only valid characters.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Gets or sets the item price. Must be a value bigger than zero.
    /// </summary>
    public decimal Price { get; private set; }
}
