using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

class SaleHasValidItemCountSpecification : ISpecification<int>
{
    public bool IsSatisfiedBy(int Quantity)
    {
        return Quantity >= 1 && Quantity <= 20;
    }
}