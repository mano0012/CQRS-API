using Ambev.DeveloperEvaluation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Guid ItemId { get; private set; } = Guid.Empty;
    public string ItemName { get; private set; } = string.Empty;
    public int Quantity { get; private set; } = 0;
    public bool Cancelled { get; private set; } = false;
    public decimal UnitPrice { get; private set; } = 0;
    public decimal Discount { get; private set; } = 0;
    public decimal TotalPrice => Quantity * UnitPrice;
    public decimal TotalPriceWithDiscount => TotalPrice - Discount;

    public SaleItem(Guid itemId, string itemName, int quantity, decimal unitPrice)
    {
        ValidateQuantity(quantity);

        Id = Guid.NewGuid();
        ItemId = itemId;
        ItemName = itemName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Cancelled = false;
        Discount = CalculateDiscount();
    }
    public SaleItem() { }

    private decimal CalculateDiscount()
    {
        //Less than 4: No discount
        if (Quantity < 4) 
            return 0;

        //Between 4 and 10: 10% discount
        if (Quantity < 10) 
            return TotalPrice * (decimal)0.1;

        //Between 10-20: 20% discount
        return TotalPrice * (decimal)0.2;
    }

    private void ValidateQuantity(int quantity)
    {
        if (quantity <= 0 || quantity > 20)
            throw new DomainException("Quantity must be between 1 and 20");
    }

    public void ChangeQuantity(int newQuantity)
    {
        ValidateQuantity(newQuantity);

        Quantity = newQuantity;
        Discount = CalculateDiscount();
    }
}