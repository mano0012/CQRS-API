using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByCustomer;

public class GetSaleByCustomerQueryValidator: AbstractValidator<GetSaleByCustomerQuery>
{
    public GetSaleByCustomerQueryValidator()
    {
        RuleFor(item => item.CustomerId)
            .NotEmpty().WithMessage("Customer Id required.");
    }
}
