using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleResult
{
    public Guid Id { get; private set; } = Guid.Empty;
    public Guid CustomerId { get; private set; } = Guid.Empty;
    public string BranchName { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<DeleteSaleItemsResult> Items { get; private set; } = new List<DeleteSaleItemsResult>();
    public Boolean Cancelled { get; private set; } = false;

    public decimal TotalPrice = 0;
}
