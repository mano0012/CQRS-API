using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
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

namespace Ambev.DeveloperEvaluation.Unit.Application.Handlers;
public class UpdateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateSaleHandler(_saleRepository, _userRepository, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
    {
        var command = new UpdateSaleCommand(); // inválido
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenSaleNotFound()
    {
        var command = new UpdateSaleCommand
        {
            SaleId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            BranchName = "Branch X"
        };

        _saleRepository.GetByIdAsync(command.SaleId, false, Arg.Any<CancellationToken>())
            .Returns((Sale)null);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Sale {command.SaleId} not found.");
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenCustomerNotFound()
    {
        var command = new UpdateSaleCommand
        {
            SaleId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            BranchName = "Branch X"
        };

        var sale = Substitute.For<Sale>(command.CustomerId, "Branch X", new List<SaleItem> { SalesHandlerTestData.GenerateValidSaleItem() });

        _saleRepository.GetByIdAsync(command.SaleId, false, Arg.Any<CancellationToken>())
            .Returns(sale);

        _userRepository.GetByIdAsync(command.CustomerId).Returns((User)null);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Customer {command.CustomerId} not found.");
    }

    [Fact]
    public async Task Handle_ShouldUpdateSaleAndReturnResult_WhenValidCommand()
    {
        var command = new UpdateSaleCommand
        {
            SaleId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            BranchName = "Branch X"
        };

        var sale = Substitute.For<Sale>(command.CustomerId, "Branch X", new List<SaleItem> { SalesHandlerTestData.GenerateValidSaleItem() });
        _saleRepository.GetByIdAsync(command.SaleId, false, Arg.Any<CancellationToken>())
            .Returns(sale);

        var user = new User { Id = command.CustomerId };
        _userRepository.GetByIdAsync(command.CustomerId).Returns(user);

        var expectedResult = new UpdateSaleResult { Id = command.SaleId, CustomerId = command.CustomerId, BranchName = command.BranchName };
        _mapper.Map<UpdateSaleResult>(sale).Returns(expectedResult);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().BeEquivalentTo(expectedResult);
        sale.Received(1).UpdateCustomer(command.CustomerId);
        sale.Received(1).UpdateBranch(command.BranchName);
        await _saleRepository.Received(1).UpdateSaleAsync(sale);
    }
}
