using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;

public class GetSaleByIdRequestValidator : AbstractValidator<GetSaleByIdRequest>
{
    public GetSaleByIdRequestValidator()
    {
        RuleFor(item => item.SaleId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Sale is required.");
    }
}