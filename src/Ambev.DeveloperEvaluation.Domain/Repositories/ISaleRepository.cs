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
    /// Retrieves a sale by their Id and CustomerId, for customer dont see other customers sales
    /// </summary>
    /// <param name="saleId">The sale id to search for</param>
    /// <param name="CustomerId">The customer id to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    Task<Sale?> GetByIdAndCustomerAsync(Guid saleId, Guid CustomerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale by their Id
    /// </summary>
    /// <param name="saleId">The sale id to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    Task<Sale?> GetByIdAsync(Guid saleId, bool trackItems = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all sales by their customer
    /// </summary>
    /// <param name="CustomerId">The customer id to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale list if found, empty otherwise</returns>
    Task<List<Sale>> GetByCustomerAsync(Guid CustomerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a sales. If is tracked, just save, if not, track and save.
    /// </summary>
    /// <param name="sale">The sale to be updated</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale</returns>
    /// 

    Task<Sale> UpdateSaleAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale item by their Id
    /// </summary>
    /// <param name="saleId">The sale id to search for</param>
    /// <param name="itemId">The item id to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The item if found, null otherwise</returns>
    Task<SaleItem?> GetSaleItemByIdAsync(Guid saleId, Guid itemId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a sale item. If is tracked, just save, if not, track and save.
    /// </summary>
    /// <param name="item">The teim to be updated</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated item</returns>
    /// 

    Task<SaleItem> UpdateItemAsync(SaleItem item, CancellationToken cancellationToken = default);
}

