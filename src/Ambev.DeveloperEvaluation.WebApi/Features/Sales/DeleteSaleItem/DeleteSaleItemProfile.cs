using Ambev.DeveloperEvaluation.Application.Sales.DeleteSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSaleItem;

public class DeleteSaleItemProfile : Profile
{
    public DeleteSaleItemProfile()
    {
        CreateMap<DeleteSaleItemRequest, DeleteSaleItemCommand>();
        CreateMap<DeleteSaleItemResult, DeleteSaleItemResponse>();
    }
}
