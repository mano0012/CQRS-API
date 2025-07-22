using FluentValidation;
namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CustomerId: Required
    /// - Items: Required at least one
    /// - Each Item: Quantity must be greater than zero.
    /// </remarks>
    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
        RuleFor(sale => sale.Items).NotEmpty().WithMessage("At least one item is required.");
        RuleForEach(sale => sale.Items)
        .SetValidator(new CreateSaleItemCommandValidator());
    }
}
