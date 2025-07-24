using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleResult
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string BranchName { get; set; } = string.Empty;
}
