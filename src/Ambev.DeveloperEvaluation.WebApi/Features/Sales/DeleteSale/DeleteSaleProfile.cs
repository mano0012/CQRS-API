using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

public class DeleteSaleProfile : Profile
{
    public DeleteSaleProfile()
    {
        CreateMap<DeleteSaleRequest, DeleteSaleCommand>();
        CreateMap<DeleteSaleItemsResult, DeleteSaleItemsResponse>();
        CreateMap<DeleteSaleResult, DeleteSaleResponse>();
    }
}
