using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSaleItem;
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

public class DeleteSaleItemHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly DeleteSaleItemHandler _handler;

    public DeleteSaleItemHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new DeleteSaleItemHandler(_saleRepository, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
    {
        var command = new DeleteSaleItemCommand();
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenSaleItemNotFound()
    {
        var command = new DeleteSaleItemCommand
        {
            SaleId = Guid.NewGuid(),
            ItemId = Guid.NewGuid()
        };

        _saleRepository.GetSaleItemByIdAsync(command.SaleId, command.ItemId, Arg.Any<CancellationToken>())
            .Returns((SaleItem)null);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Item {command.ItemId} not found.");
    }

    [Fact]
    public async Task Handle_ShouldCancelItemAndReturnResult_WhenSaleItemExists()
    {
        var command = new DeleteSaleItemCommand
        {
            SaleId = Guid.NewGuid(),
            ItemId = Guid.NewGuid()
        };

        var saleItem = Substitute.For<SaleItem>(command.ItemId, "Product X", 1, 10m, new NoDiscountStrategy());

        _saleRepository.GetSaleItemByIdAsync(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(saleItem);

        var expectedResult = new DeleteSaleItemResult
        {
            ItemId = saleItem.ItemId,
            ItemName = saleItem.ItemName,
            Quantity = saleItem.Quantity,
            Cancelled = true,
            UnitPrice = saleItem.UnitPrice,
            Discount = saleItem.Discount
        };

        _mapper.Map<DeleteSaleItemResult>(saleItem).Returns(expectedResult);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().BeEquivalentTo(expectedResult);
        await _saleRepository.Received(1).UpdateItemAsync(saleItem);
        saleItem.Received(1).Cancel();
    }
}

