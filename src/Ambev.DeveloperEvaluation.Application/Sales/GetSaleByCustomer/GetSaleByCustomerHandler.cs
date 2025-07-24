using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByCustomer;

public class GetSaleByCustomerHandler : IRequestHandler<GetSaleByCustomerQuery, List<GetSaleByCustomerResult>>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    public GetSaleByCustomerHandler(ISaleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetSaleByCustomerResult>> Handle(GetSaleByCustomerQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleByCustomerQueryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var saleList = await _repository.GetByCustomerAsync(request.CustomerId);
        
        var data = _mapper.Map<List<GetSaleByCustomerResult>>(saleList);
        return data;

    }

}