using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Strategies.Discount;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateSaleHandlerTests
{
    private readonly IItemRepository _itemRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _itemRepository = Substitute.For<IItemRepository>();
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_saleRepository, _itemRepository, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
    {
        var command = new CreateSaleCommand();
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldThrowDomainException_WhenItemNotFound()
    {
        var command = new CreateSaleCommand
        {
            CustomerId = Guid.NewGuid(),
            BranchName = "Branch A",
            Items = new List<CreateSaleItemCommand>
            {
                new CreateSaleItemCommand { ItemId = Guid.NewGuid(), Quantity = 2 }
            }
        };

        _itemRepository.GetByIdAsync(Arg.Any<Guid>()).Returns((Item)null);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage($"Item {command.Items[0].ItemId} not found");
    }

    [Fact]
    public async Task Handle_ShouldCreateSaleAndReturnResult_WhenCommandIsValid()
    {
        var command = SalesHandlerTestData.GenerateSaleValidCommand();
        var item = SalesHandlerTestData.GenerateValidItem();

        _itemRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(item);

        SaleItem saleItem = SalesHandlerTestData.GenerateValidSaleItem();

        var createdSale = new Sale(
            customer: command.CustomerId, 
            branch: command.BranchName, 
            items: new List<SaleItem> { saleItem });

        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(createdSale);

        var expectedResult = new CreateSaleResult { Id = Guid.NewGuid() };
        _mapper.Map<CreateSaleResult>(createdSale).Returns(expectedResult);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().BeEquivalentTo(expectedResult);
        await _saleRepository.Received(1)
            .CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }
}