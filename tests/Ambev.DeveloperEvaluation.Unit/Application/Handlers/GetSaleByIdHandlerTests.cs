using Ambev.DeveloperEvaluation.Application.Sales.GetSaleByCustomer;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Strategies.Discount;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Handlers;

public class GetSaleByIdHandlerTests
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly GetSaleByIdHandler _handler;

    public GetSaleByIdHandlerTests()
    {
        _repository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleByIdHandler(_repository, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenQueryIsInvalid()
    {
        var query = new GetSaleByIdQuery();
        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenSaleNotFound()
    {
        var query = new GetSaleByIdQuery
        {
            SaleId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid()
        };

        _repository.GetByIdAndCustomerAsync(query.SaleId, query.CustomerId)
            .Returns((Sale)null);

        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Sale {query.SaleId} not found.");
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedResult_WhenSaleExists()
    {
        var query = new GetSaleByIdQuery
        {
            SaleId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid()
        };

        var item = GenerateValidItem();

        SaleItem saleItem = new SaleItem(
            itemId: item.Id,
            itemName: item.Name,
            quantity: 2,
            unitPrice: 10,
            discountStrategy: new NoDiscountStrategy());

        Sale sale = Substitute.For<Sale>(query.CustomerId, "Branch X", new List<SaleItem> { saleItem });

        _repository.GetByIdAndCustomerAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
            .Returns(sale);

        var expectedItemResult = new GetSaleItemByIdResult
        {
            ItemId = saleItem.ItemId,
            ItemName = saleItem.ItemName,
            Quantity = saleItem.Quantity,
            Cancelled = true,
            UnitPrice = saleItem.UnitPrice,
            Discount = saleItem.Discount
        };

        var expectedResult = new GetSaleByIdResult
        {
            Id = sale.Id,
            CustomerId = sale.CustomerId,
            BranchName = sale.BranchName,
            CreatedAt = sale.CreatedAt,
            Cancelled = true,
            Items = new List<GetSaleItemByIdResult>
            {
                expectedItemResult
            }
        };

        _mapper.Map<GetSaleByIdResult>(sale).Returns(expectedResult);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeEquivalentTo(expectedResult);
        await _repository.Received(1).GetByIdAndCustomerAsync(query.SaleId, query.CustomerId);
    }

    private Item GenerateValidItem()
    {
        var item = new Item("Product A", 10m);
        item.Id = Guid.NewGuid();
        return item;
    }
}
