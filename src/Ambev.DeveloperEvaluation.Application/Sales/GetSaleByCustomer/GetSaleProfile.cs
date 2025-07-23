using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByCustomer;

public class GetSaleByIdProfile : Profile
{
    public GetSaleByIdProfile()
    {
        CreateMap<SaleItem, GetSaleItemByCustomerResult>();
        CreateMap<Sale, GetSaleByCustomerResult>();
    }
}