using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, GetSaleByIdResult>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    public GetSaleByIdHandler(ISaleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetSaleByIdResult> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleByIdQueryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _repository.GetByIdAndCustomerAsync(request.SaleId, request.CustomerId);

        if(sale == null)
        {
            throw new NotFoundException($"Sale {request.SaleId} not found.");
        }

        return _mapper.Map<GetSaleByIdResult>(sale);
    }
}