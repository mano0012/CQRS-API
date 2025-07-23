using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;

public class GetSaleByIdProfile : Profile
{
    public GetSaleByIdProfile()
    {
        CreateMap<GetSaleByIdRequest, GetSaleByIdQuery>();
        CreateMap<GetSaleItemByIdResult, GetSaleItemByIdResponse>();
        CreateMap<GetSaleByIdResult, GetSaleByIdResponse>();
    }
}