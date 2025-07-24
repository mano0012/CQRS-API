using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSaleItem;

public class DeleteSaleItemProfile : Profile
{
    public DeleteSaleItemProfile()
    {
        CreateMap<SaleItem, DeleteSaleItemResult>();
    }
}
