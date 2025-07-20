using Ambev.DeveloperEvaluation.Application.Items.CreateItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Items.GetItem;

public class GetItemProfile : Profile
{
    public GetItemProfile()
    {
        CreateMap<Item, GetItemResult>();
    }
}
