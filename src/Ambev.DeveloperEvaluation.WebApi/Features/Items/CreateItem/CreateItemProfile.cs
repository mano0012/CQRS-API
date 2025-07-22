using Ambev.DeveloperEvaluation.Application.Items.CreateItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Items.CreateItem;

public class CreateItemProfile : Profile
{
    public CreateItemProfile()
    {
        CreateMap<CreateItemRequest, CreateItemCommand>();
        CreateMap<CreateItemResult, CreateItemResponse>();
    }
}
