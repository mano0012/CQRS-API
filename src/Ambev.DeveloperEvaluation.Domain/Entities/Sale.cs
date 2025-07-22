using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public Guid CustomerId { get; private set; } = Guid.Empty;
    public string BranchName { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<SaleItem> Items { get; private set; } = new List<SaleItem>();
    public Boolean Cancelled { get; private set; } = false;
    
    public Sale(Guid customer, string branch, List<SaleItem> items)
    {
        if(customer == Guid.Empty)
            throw new DomainException("A sale must contain a customer ID.");

        if (items.Count == 0)
            throw new DomainException("A sale must contain at least one item.");

        Id = Guid.NewGuid();
        CustomerId = customer;
        BranchName = branch;
        Items = items;
        CreatedAt = DateTime.UtcNow;
        Cancelled = false;
        AddDomainEvent(new SaleCreatedEvent(Id, CustomerId));
    }

    public Sale()
    { }
}
