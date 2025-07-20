namespace Ambev.DeveloperEvaluation.WebApi.Features.Items.GetItem;

public class GetItemResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created item.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created item in the system.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the item to be created.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price for the item.
    /// </summary>
    public decimal Price { get; set; } = 0;
}
