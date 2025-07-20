using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Items.CreateItem;

public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateItemCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Required, length between 3 and 50 characters
    /// - Price: Must be bigger than zero
    /// </remarks>
    public CreateItemCommandValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.");
        RuleFor(item => item.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}