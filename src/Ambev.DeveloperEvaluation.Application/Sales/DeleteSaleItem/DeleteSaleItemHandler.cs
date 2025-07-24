using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSaleItem;

public class DeleteSaleItemHandler : IRequestHandler<DeleteSaleItemCommand, DeleteSaleItemResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public DeleteSaleItemHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the DeleteSaleCommand request
    /// </summary>
    /// <param name="command">The DeleteSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The delete sale details</returns>
    public async Task<DeleteSaleItemResult> Handle(DeleteSaleItemCommand command, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleItemCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var saleItem = await _saleRepository.GetSaleItemByIdAsync(command.SaleId, command.ItemId, cancellationToken);

        if (saleItem == null)
        {
            throw new NotFoundException($"Item {command.ItemId} not found.");
        }

        saleItem.Cancel();
        
        await _saleRepository.UpdateItemAsync(saleItem);

        return _mapper.Map<DeleteSaleItemResult>(saleItem);
    }
}
