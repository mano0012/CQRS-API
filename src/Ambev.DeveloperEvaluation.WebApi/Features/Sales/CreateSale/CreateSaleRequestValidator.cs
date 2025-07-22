using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Items: Required at least one
    /// - Each Item: Quantity must be greater than zero.
    /// </remarks>
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.Items).NotEmpty().WithMessage("At least one item is required.");
        RuleForEach(sale => sale.Items)
        .SetValidator(new CreateSaleItemRequestValidator());

    }
}
