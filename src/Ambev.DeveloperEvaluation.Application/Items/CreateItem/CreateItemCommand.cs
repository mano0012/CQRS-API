namespace Ambev.DeveloperEvaluation.Application.Items.CreateItem;

using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

/// <summary>
/// Command for creating a new item.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating an item, 
/// including name and price.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateItemResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateItemCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateItemCommand : IRequest<CreateItemResult>
{
    /// <summary>
    /// Gets or sets the name of the item to be created.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price for the item.
    /// </summary>
    public decimal Price { get; set; } = 0;

    public ValidationResultDetail Validate()
    {
        var validator = new CreateItemCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}