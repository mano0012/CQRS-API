using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IItemRepository
{
    /// <summary>
    /// Creates a new item in the repository
    /// </summary>
    /// <param name="item">The item to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created item</returns>
    Task<Item> CreateAsync(Item item, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a item by their name
    /// </summary>
    /// <param name="name">The name to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The item if found, null otherwise</returns>
    Task<Item?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a item by their ID
    /// </summary>
    /// <param name="id">The ID to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The item if found, null otherwise</returns>
    Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all items
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The item list</returns>
    Task<IEnumerable<Item>> GetAllItemsAsync(CancellationToken cancellationToken = default);

    ///// <summary>
    ///// Deletes a user from the repository
    ///// </summary>
    ///// <param name="id">The unique identifier of the user to delete</param>
    ///// <param name="cancellationToken">Cancellation token</param>
    ///// <returns>True if the user was deleted, false if not found</returns>
    //Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

