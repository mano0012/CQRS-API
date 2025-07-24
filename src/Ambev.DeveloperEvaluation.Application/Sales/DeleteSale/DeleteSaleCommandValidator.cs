using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

class DeleteSaleCommandValidator : AbstractValidator<DeleteSaleCommand>
{
    public DeleteSaleCommandValidator()
    {
        RuleFor(item => item.SaleId)
            .NotEmpty().WithMessage("Sale Id required.");
    }
}