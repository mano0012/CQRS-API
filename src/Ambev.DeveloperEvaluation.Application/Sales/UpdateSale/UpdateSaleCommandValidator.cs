using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id (Sale): Required
    /// </remarks>
    public UpdateSaleCommandValidator()
    {
        RuleFor(sale => sale.SaleId).NotEmpty().WithMessage("Sale Id is required.");
    }
}