using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdQueryValidator: AbstractValidator<GetSaleByIdQuery>
{
    public GetSaleByIdQueryValidator()
    {
        RuleFor(item => item.SaleId)
            .NotEmpty().WithMessage("Sale Id required.");
        RuleFor(item => item.CustomerId)
            .NotEmpty().WithMessage("Customer Id required.");
    }
}
