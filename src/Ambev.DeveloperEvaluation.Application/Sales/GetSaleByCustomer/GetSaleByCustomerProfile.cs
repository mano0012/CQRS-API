using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByCustomer;

public class GetSaleByCustomerProfile : Profile
{
    public GetSaleByCustomerProfile()
    {
        CreateMap<SaleItem, GetSaleItemByCustomerResult>();
        CreateMap<Sale, GetSaleByCustomerResult>();
    }
}