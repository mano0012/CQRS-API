using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Strategies.Discount;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public UpdateSaleHandler(ISaleRepository saleRepository, IUserRepository userRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the UpdateSaleCommand request
    /// </summary>
    /// <param name="command">The UpdateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale details</returns>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(command.SaleId, false, cancellationToken);

        if (sale == null)
        {
            throw new NotFoundException($"Sale {command.SaleId} not found.");
        }

        if(command.CustomerId != Guid.Empty)
        {
            var user = await _userRepository.GetByIdAsync(command.CustomerId);

            if(user == null)
                throw new NotFoundException($"Customer {command.CustomerId} not found.");
        }

        sale.UpdateCustomer(command.CustomerId);
        sale.UpdateBranch(command.BranchName);

        await _saleRepository.UpdateSaleAsync(sale);

        return _mapper.Map<UpdateSaleResult>(sale);
    }
}

