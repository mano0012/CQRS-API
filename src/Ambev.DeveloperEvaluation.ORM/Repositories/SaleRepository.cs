using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;


public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sale.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    
    public async Task<Sale?> GetByIdAsync(Guid saleId, Guid CustomerId, CancellationToken cancellationToken = default)
    {
        return await _context.Sale
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == saleId && s.CustomerId == CustomerId, cancellationToken);
    }

    public async Task<List<Sale>> GetByCustomerAsync(Guid CustomerId, CancellationToken cancellationToken = default)
    {
        return await _context.Sale
        .Include(s => s.Items)
        .Where(s => s.CustomerId == CustomerId)
        .ToListAsync(cancellationToken);
    }

}
