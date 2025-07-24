using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Strategies.Discount;

public class NoDiscountStrategy : IDiscountStrategy
{
    public decimal CalculateDiscount(SaleItem item)
    {
        return 0;
    }
}

