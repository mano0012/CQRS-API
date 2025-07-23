using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleByCustomer;

public class GetSaleByCustomerRequestValidator : AbstractValidator<GetSaleByCustomerRequest>
{
    public GetSaleByCustomerRequestValidator()
    {
        RuleFor(item => item.CustomerId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Customer Id is required.");
    }
}