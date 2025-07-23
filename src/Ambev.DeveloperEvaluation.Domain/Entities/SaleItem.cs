using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Strategies.Discount;
using Ambev.DeveloperEvaluation.Domain.Specifications;

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

    private IDiscountStrategy _discountStrategy;
    public SaleItem(Guid itemId, string itemName, int quantity, decimal unitPrice, IDiscountStrategy discountStrategy)
    {
        ValidateQuantity(quantity);

        Id = Guid.NewGuid();
        ItemId = itemId;
        ItemName = itemName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Cancelled = false;
        _discountStrategy = discountStrategy;
        Discount = CalculateDiscount();
    }
    public SaleItem() { }

    private decimal CalculateDiscount()
    {
        if(_discountStrategy == null)
            throw new ArgumentNullException("Discount Strategy not set");

        return _discountStrategy.CalculateDiscount(this);

    }

    public void ChangeQuantity(int newQuantity)
    {
        ValidateQuantity(newQuantity);

        Quantity = newQuantity;
        Discount = CalculateDiscount();
    }

    public void ChangeDiscountPolicy(IDiscountStrategy policy)
    {
        _discountStrategy = policy;
    }

    private void ValidateQuantity(int quantity)
    {
        var quantitySpecification = new SaleHasValidItemCountSpecification();

        if (!quantitySpecification.IsSatisfiedBy(quantity))
            throw new DomainException("Quantity must be between 1 and 20");
    }

    public void CancelItem()
    {
        if (!Cancelled)
        {
            Cancelled = true;
            AddDomainEvent(new SaleItemCancelledEvent(Id));
        }
    }
}