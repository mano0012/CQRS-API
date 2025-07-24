using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSaleItem;

class DeleteSaleItemCommandValidator : AbstractValidator<DeleteSaleItemCommand>
{
    public DeleteSaleItemCommandValidator()
    {
        RuleFor(item => item.SaleId)
            .NotEmpty().WithMessage("Sale Id required.");

        RuleFor(item => item.ItemId)
            .NotEmpty().WithMessage("Item Id required.");
    }
}