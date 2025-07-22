using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of ItemRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ItemRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new item in the database
    /// </summary>
    /// <param name="item">The item to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created item</returns>
    public async Task<Item> CreateAsync(Item item, CancellationToken cancellationToken = default)
    {
        await _context.Items.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return item;
    }

    /// <summary>
    /// Retrieves an item by their name
    /// </summary>
    /// <param name="email">The name to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The item if found, null otherwise</returns>
    public async Task<Item?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Items
            .FirstOrDefaultAsync(u => u.Name == name, cancellationToken);
    }

    /// <summary>
    /// Retrieves all items
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The item list</returns>
    public async Task<IEnumerable<Item>> GetAllItemsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Items.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Items
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
}
