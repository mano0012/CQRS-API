using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    /// <summary>
    /// Creates a new sale in the repository
    /// </summary>
    /// <param name="sale">The sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale</returns>
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

    ///// <summary>
    ///// Retrieves a item by their name
    ///// </summary>
    ///// <param name="name">The name to search for</param>
    ///// <param name="cancellationToken">Cancellation token</param>
    ///// <returns>The item if found, null otherwise</returns>
    //Task<Item?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    ///// <summary>
    ///// Retrieves a item by their ID
    ///// </summary>
    ///// <param name="id">The ID to search for</param>
    ///// <param name="cancellationToken">Cancellation token</param>
    ///// <returns>The item if found, null otherwise</returns>
    //Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ///// <summary>
    ///// Retrieves all items
    ///// </summary>
    ///// <param name="cancellationToken">Cancellation token</param>
    ///// <returns>The item list</returns>
    //Task<IEnumerable<Item>> GetAllItemsAsync(CancellationToken cancellationToken = default);
}

