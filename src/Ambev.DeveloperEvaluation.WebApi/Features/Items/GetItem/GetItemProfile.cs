using Ambev.DeveloperEvaluation.Application.Items.CreateItem;
using Ambev.DeveloperEvaluation.Application.Items.GetItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Items.CreateItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Items.GetItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Items.GetItems;

public class GetItemProfile : Profile
{
    public GetItemProfile()
    {
        CreateMap<GetItemResult, GetItemResponse>();
    }
}

