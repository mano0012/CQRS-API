using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(item => item.SaleId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Sale is required.");
    }
}
