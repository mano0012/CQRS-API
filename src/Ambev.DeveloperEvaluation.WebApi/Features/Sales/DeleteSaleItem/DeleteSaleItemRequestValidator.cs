using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSaleItem;

public class DeleteSaleItemRequestValidator : AbstractValidator<DeleteSaleItemRequest>
{
    public DeleteSaleItemRequestValidator()
    {
        RuleFor(item => item.SaleId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Sale is required.");

        RuleFor(item => item.ItemId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Item is required.");
    }
}
