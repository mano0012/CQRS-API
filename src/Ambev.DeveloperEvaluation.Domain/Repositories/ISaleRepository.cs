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

    /// <summary>
    /// Retrieves a sale by their Id, ensuring that users are not going to see other users sales
    /// </summary>
    /// <param name="saleId">The sale id to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    Task<Sale?> GetByIdAsync(Guid saleId, Guid CustomerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all sales by their customer
    /// </summary>
    /// <param name="CustomerId">The customer id to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale list if found, empty otherwise</returns>
    Task<List<Sale>> GetByCustomerAsync(Guid CustomerId, CancellationToken cancellationToken = default);
}

