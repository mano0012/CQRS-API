using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

public class DeleteSaleRequestValidator : AbstractValidator<DeleteSaleRequest>
{
    public DeleteSaleRequestValidator()
    {
        RuleFor(item => item.SaleId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Sale is required.");
    }
}
