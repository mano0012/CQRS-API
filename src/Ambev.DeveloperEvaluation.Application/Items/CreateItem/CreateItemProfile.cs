using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Items.CreateItem;

public class CreateItemProfile : Profile
{
    public CreateItemProfile()
    {
        CreateMap<CreateItemCommand, Item>();
        CreateMap<Item, CreateItemResult>();
    }
}
