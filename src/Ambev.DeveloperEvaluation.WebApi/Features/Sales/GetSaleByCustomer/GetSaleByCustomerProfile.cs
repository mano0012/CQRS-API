using Ambev.DeveloperEvaluation.Application.Sales.GetSaleByCustomer;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleByCustomer;

public class GetSaleByCustomerProfile : Profile
{
    public GetSaleByCustomerProfile()
    {
        CreateMap<GetSaleByCustomerRequest, GetSaleByCustomerQuery>();
        CreateMap<GetSaleItemByCustomerResult, GetSaleItemByCustomerResponse>();
        CreateMap<GetSaleByCustomerResult, GetSaleByCustomerResponse>();
    }
}