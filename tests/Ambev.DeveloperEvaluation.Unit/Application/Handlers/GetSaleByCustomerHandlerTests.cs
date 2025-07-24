using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Application.Handlers;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleByCustomer;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Strategies.Discount;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

public class GetSaleByCustomerHandlerTests
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly GetSaleByCustomerHandler _handler;

    public GetSaleByCustomerHandlerTests()
    {
        _repository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleByCustomerHandler(_repository, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenQueryIsInvalid()
    {
        var query = new GetSaleByCustomerQuery();
        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedResults_WhenValidQuery()
    {
        var query = new GetSaleByCustomerQuery { CustomerId = Guid.NewGuid() };

        var item = GenerateValidItem();

        SaleItem saleItem = new SaleItem(
            itemId: item.Id,
            itemName: item.Name,
            quantity: 2,
            unitPrice: 10,
            discountStrategy: new NoDiscountStrategy());

        Sale sale = Substitute.For<Sale>(query.CustomerId, "Branch X", new List<SaleItem> { saleItem });

        var saleList = new List<Sale>
        {
            sale
        };

        _repository.GetByCustomerAsync(query.CustomerId).Returns(saleList);

        var expectedItemResult = new GetSaleItemByCustomerResult
        {
            ItemId = saleItem.ItemId,
            ItemName = saleItem.ItemName,
            Quantity = saleItem.Quantity,
            Cancelled = true,
            UnitPrice = saleItem.UnitPrice,
            Discount = saleItem.Discount
        };

        var expectedResult = new GetSaleByCustomerResult
        {
            Id = sale.Id,
            CustomerId = sale.CustomerId,
            BranchName = sale.BranchName,
            CreatedAt = sale.CreatedAt,
            Cancelled = true,
            Items = new List<GetSaleItemByCustomerResult>
            {
                expectedItemResult
            }
        };

        var expectedResultsList = new List<GetSaleByCustomerResult>
        {
            expectedResult
        };
        _mapper.Map<List<GetSaleByCustomerResult>>(saleList).Returns(expectedResultsList);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeEquivalentTo(expectedResultsList);
        await _repository.Received(1).GetByCustomerAsync(query.CustomerId);
    }

    private Item GenerateValidItem()
    {
        var item = new Item("Product A", 10m);
        item.Id = Guid.NewGuid();
        return item;
    }

}

