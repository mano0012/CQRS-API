using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Application.Handlers;

using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Strategies.Discount;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly DeleteSaleHandler _handler;

    public DeleteSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new DeleteSaleHandler(_saleRepository, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
    {
        var command = new DeleteSaleCommand();
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenSaleNotFound()
    {
        var command = new DeleteSaleCommand { SaleId = Guid.NewGuid() };

        _saleRepository.GetByIdAsync(command.SaleId, true, Arg.Any<CancellationToken>())
            .Returns((Sale)null);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Sale {command.SaleId} not found.");
    }

    [Fact]
    public async Task Handle_ShouldCancelSaleAndReturnResult_WhenSaleExists()
    {
        var command = new DeleteSaleCommand { SaleId = Guid.NewGuid() };

        SaleItem saleItem = SalesHandlerTestData.GenerateValidSaleItem();

        var sale = Substitute.For<Sale>(Guid.NewGuid(), "Branch X", new List<SaleItem> { saleItem });

        _saleRepository.GetByIdAsync(command.SaleId, true, Arg.Any<CancellationToken>())
            .Returns(sale);

        var expectedItemResult = new DeleteSaleItemsResult
        {
            ItemId = saleItem.ItemId,
            ItemName = saleItem.ItemName,
            Quantity = saleItem.Quantity,
            Cancelled = true,
            UnitPrice = saleItem.UnitPrice,
            Discount = saleItem.Discount
        };

        var expectedResult = new DeleteSaleResult
        {
            Id = sale.Id,
            CustomerId = sale.CustomerId,
            BranchName = sale.BranchName,
            CreatedAt = sale.CreatedAt,
            Cancelled = true,
            Items = new List<DeleteSaleItemsResult>
            {
                expectedItemResult
            }
        };

        _mapper.Map<DeleteSaleResult>(sale).Returns(expectedResult);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().BeEquivalentTo(expectedResult);
        await _saleRepository.Received(1).UpdateSaleAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }
}
